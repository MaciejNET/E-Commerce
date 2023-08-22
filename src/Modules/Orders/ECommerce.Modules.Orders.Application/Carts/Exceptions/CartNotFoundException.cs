using ECommerce.Shared.Abstractions.Exceptions;

namespace ECommerce.Modules.Orders.Application.Carts.Exceptions;

public sealed class CartNotFoundException : ECommerceException
{
    public Guid UserId { get; }

    public CartNotFoundException(Guid userId) : base($"Cart for user with ID: '{userId}' was not found.")
    {
        UserId = userId;
    }
}