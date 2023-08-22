using ECommerce.Modules.Orders.Domain.Orders.Exceptions;

namespace ECommerce.Modules.Orders.Domain.Orders.ValueObjects;

public record OrderLine
{
    public int OrderLineNumber { get; private set; }
    public string Sku { get; private set; }
    public string Name { get; private set; }
    public decimal UnitPrice { get; private set; }
    public int Quantity { get; private set; }

    public OrderLine(int orderLineNumber, string sku, string name, decimal unitPrice, int quantity)
    {
        if (quantity < 1)
        {
            throw new InvalidOrderLineException(nameof(quantity));
        }

        if (unitPrice < 0)
        {
            throw new InvalidOrderLineException(nameof(unitPrice));
        }
        
        OrderLineNumber = orderLineNumber;
        Sku = sku;
        Name = name;
        UnitPrice = unitPrice;
        Quantity = quantity;
    }
}