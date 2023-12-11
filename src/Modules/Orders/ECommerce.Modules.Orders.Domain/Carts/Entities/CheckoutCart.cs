using ECommerce.Modules.Orders.Domain.Carts.Events;
using ECommerce.Modules.Orders.Domain.Carts.Exceptions;
using ECommerce.Modules.Orders.Domain.Orders.Exceptions;
using ECommerce.Modules.Orders.Domain.Shared.Enums;
using ECommerce.Modules.Orders.Domain.Shared.ValueObjects;
using ECommerce.Shared.Abstractions.Kernel.Enums;
using ECommerce.Shared.Abstractions.Kernel.Types;
using ECommerce.Shared.Abstractions.Time;

namespace ECommerce.Modules.Orders.Domain.Carts.Entities;

public class CheckoutCart : AggregateRoot
{
    private readonly List<CheckoutCartItem> _items;
    
    public UserId UserId { get; private set; }
    public PaymentMethod Payment { get; private set; }
    public Shipment Shipment { get; private set; }
    public Discount Discount { get; private set; }
    public Currency Currency { get; private set; }
    public decimal TotalPrice => _items.Sum(x => x.Price.Amount);
    public IReadOnlyCollection<CheckoutCartItem> Items => _items.AsReadOnly();

    internal CheckoutCart(Cart cart, List<CheckoutCartItem> items, AggregateId? id = null)
    {
        Id = id ?? new AggregateId(Guid.NewGuid());
        UserId = cart.UserId;
        _items = items;
        Currency = cart.PreferredCurrency;
    }

    public void SetPayment(PaymentMethod payment)
    {
        Payment = payment;
        IncrementVersion();
    }

    public void SetShipment(Shipment shipment)
    {
        Shipment = shipment;
        IncrementVersion();
    }

    public void ApplyDiscount(Discount discount)
    {
        bool isApplied = false;
        foreach (var item in Items)
        {
            isApplied = item.ApplyDiscount(discount);
        }

        if (!isApplied)
        {
            throw new DiscountApplicationException();
        }
        
        Discount = discount;
        IncrementVersion();
    }

    public void PlaceOrder(IClock clock, AggregateId? orderId = null)
    {
        foreach (var item in Items)
        {
            if (item.Product.StockQuantity - item.Quantity < 0)
            {
                ClearEvents();
                throw new NotEnoughProductsInStockException(item.Product.Id);
            }
            AddEvent(new ProductBought(item.Product.Id, item.Quantity));
        }
        
        AddEvent(new OrderPlaced(this, clock.CurrentDate(), orderId));
        AddEvent(new CartCheckoutProcessed(UserId));
        IncrementVersion();
    }
    
    public CheckoutCart() {}
}