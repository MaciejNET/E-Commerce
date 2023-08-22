using ECommerce.Shared.Abstractions.Exceptions;

namespace ECommerce.Modules.Orders.Application.Carts.Exceptions;

public sealed class ProductNotFoundException : ECommerceException
{
    public Guid ProductId { get; }

    public ProductNotFoundException(Guid productId) : base($"Product with ID: '{productId}' was not found.")
    {
        ProductId = productId;
    }
}