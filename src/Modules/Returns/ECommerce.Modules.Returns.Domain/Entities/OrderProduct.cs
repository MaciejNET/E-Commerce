using ECommerce.Shared.Abstractions.Kernel.Types;

namespace ECommerce.Modules.Returns.Domain.Entities;

public class OrderProduct
{
    public EntityId Id { get; private set; }
    public string Sku { get; private set; }
    public AggregateId OrderId { get; private set; }
    public bool IsReturn { get; private set; }

    private OrderProduct(string sku, AggregateId orderId)
    {
        Id = new EntityId(Guid.NewGuid());
        Sku = sku;
        OrderId = orderId;
        IsReturn = false;
    }

    public static OrderProduct Create(string sku, AggregateId orderId) => new(sku, orderId);

    public void Return() => IsReturn = true;
    
    private OrderProduct() {}
}