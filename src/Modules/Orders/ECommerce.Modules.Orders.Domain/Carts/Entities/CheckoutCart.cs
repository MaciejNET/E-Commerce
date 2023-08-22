using ECommerce.Modules.Orders.Domain.Carts.Events;
using ECommerce.Modules.Orders.Domain.Carts.Exceptions;
using ECommerce.Modules.Orders.Domain.Orders.Exceptions;
using ECommerce.Modules.Orders.Domain.Shared.Entities;
using ECommerce.Modules.Orders.Domain.Shared.ValueObjects;
using ECommerce.Shared.Abstractions.Kernel.Types;
using ECommerce.Shared.Abstractions.Time;

namespace ECommerce.Modules.Orders.Domain.Carts.Entities;

public class CheckoutCart : AggregateRoot
{
    private readonly List<CartItem> _items;
    
    public UserId UserId { get; private set; }
    public PaymentMethod Payment { get; private set; }
    public Shipment Shipment { get; private set; }
    public Discount Discount { get; private set; }
    public IReadOnlyCollection<CartItem> Items => _items.AsReadOnly();

    internal CheckoutCart(Cart cart, AggregateId? id = null)
    {
        Id = id ?? new AggregateId(Guid.NewGuid());
        UserId = cart.UserId;
        _items = cart.Items.ToList();
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
        if (discount.Type == DiscountType.Product)
        {
            if (!Items.Select(x => x.Product).Any(product => discount.Products.Contains(product)))
            {
                throw new DiscountApplicationException();
            }
        }
        
        Discount = discount;
        IncrementVersion();
    }

    public void PlaceOrder(IClock clock)
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
        
        AddEvent(new OrderPlaced(this, clock.CurrentDate()));
        AddEvent(new CartCheckoutProcessed(UserId));
        IncrementVersion();
    }
    
    public CheckoutCart() {}
}