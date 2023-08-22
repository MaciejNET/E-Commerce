using ECommerce.Shared.Abstractions.Exceptions;

namespace ECommerce.Modules.Orders.Domain.Carts.Exceptions;

public sealed class CartItemNotFoundException : ECommerceException
{
    public Guid Id { get; }

    public CartItemNotFoundException(Guid id) : base($"Cart item with product ID: '{id}' was not found.")
    {
        Id = id;
    }
}