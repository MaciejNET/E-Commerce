using ECommerce.Shared.Abstractions.Exceptions;

namespace ECommerce.Modules.Orders.Domain.Orders.Exceptions;

public sealed class NotEnoughProductsInStockException : ECommerceException
{
    public Guid ProductId { get; }

    public NotEnoughProductsInStockException(Guid productId)
        : base($"There is not enough stock units for product with ID: '{productId}'.")
    {
        ProductId = productId;
    }
}