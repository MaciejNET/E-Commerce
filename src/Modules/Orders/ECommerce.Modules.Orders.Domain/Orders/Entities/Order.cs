using ECommerce.Modules.Orders.Domain.Carts.Entities;
using ECommerce.Modules.Orders.Domain.Orders.Exceptions;
using ECommerce.Modules.Orders.Domain.Orders.ValueObjects;
using ECommerce.Modules.Orders.Domain.Shared.Enums;
using ECommerce.Modules.Orders.Domain.Shared.ValueObjects;
using ECommerce.Shared.Abstractions.Kernel.Enums;
using ECommerce.Shared.Abstractions.Kernel.Types;
using ECommerce.Shared.Abstractions.Time;

namespace ECommerce.Modules.Orders.Domain.Orders.Entities;

public class Order : AggregateRoot
{
    private readonly List<OrderLine> _lines;
    public UserId UserId { get; private set; }
    public IReadOnlyCollection<OrderLine> Lines => _lines.AsReadOnly();
    public Shipment Shipment { get; private set; }
    public PaymentMethod PaymentMethod { get; private set; }
    public DateTime PlaceDate { get; private set; }
    public OrderStatus Status { get; private set; }
    public DateTime? CompletionDate { get; private set; }
    public bool IsCompleted => Status is OrderStatus.Canceled or OrderStatus.Completed;

    private Order(UserId userId, IReadOnlyCollection<OrderLine> lines,  Shipment shipment,
        PaymentMethod paymentMethod, DateTime placeDate, AggregateId? id = null)
    {
        UserId = userId;
        _lines = lines.ToList();
        Shipment = shipment ?? throw new ArgumentNullException(nameof(shipment));
        PaymentMethod = paymentMethod;
        PlaceDate = placeDate;
        Status = OrderStatus.Placed;

        if (id is not null)
        {
            Id = id.Value;
        }
        else
        {
            Id = new AggregateId(Guid.NewGuid());
        }
    }

    internal static Order CreateFromCheckout(CheckoutCart checkoutCart, DateTime now, AggregateId? id = null)
    {
        var orderLines = checkoutCart.Items
            .Select((x, i) =>
            {
                var price = x.DiscountedPrice ?? x.Price;
                return new OrderLine(i, x.Product.Sku, x.Product.Name, price.Amount, checkoutCart.Currency, x.Quantity);
            })
            .ToList();

        return new Order(checkoutCart.UserId, orderLines, checkoutCart.Shipment, checkoutCart.Payment, now, id);
    }
    
    public void StartProcessing()
    {
        if (Status is not OrderStatus.Placed)
        {
            throw new InvalidOrderStatusChangeException(Status.ToString(), OrderStatus.InProgress.ToString());
        }

        Status = OrderStatus.InProgress;
    }
    
    public void Send()
    {
        if (Status is not OrderStatus.InProgress)
        {
            throw new InvalidOrderStatusChangeException(Status.ToString(), OrderStatus.Sent.ToString());
        }

        Status = OrderStatus.Sent;
    }
    
    public void Complete(DateTime now)
    {
        if (Status is not OrderStatus.Sent)
        {
            throw new InvalidOrderStatusChangeException(Status.ToString(), OrderStatus.Completed.ToString());
        }

        Status = OrderStatus.Completed;
        CompletionDate = now;
    }
    
    public void Cancel()
    {
        if (Status is OrderStatus.Completed or OrderStatus.Sent)
        {
            throw new InvalidOrderStatusChangeException(Status.ToString(), OrderStatus.Canceled.ToString());
        }

        Status = OrderStatus.Canceled;
    }

    public void PartlyReturn()
    {
        if (Status is not (OrderStatus.Completed or OrderStatus.PartlyReturned))
        {
            throw new InvalidOrderStatusChangeException(Status.ToString(), OrderStatus.PartlyReturned.ToString());
        }

        Status = OrderStatus.PartlyReturned;
    }
    
    public void Return()
    {
        if (Status is not (OrderStatus.Completed or OrderStatus.PartlyReturned))
        {
            throw new InvalidOrderStatusChangeException(Status.ToString(), OrderStatus.PartlyReturned.ToString());
        }

        Status = OrderStatus.Returned;
    }
    
    private Order() {}
}