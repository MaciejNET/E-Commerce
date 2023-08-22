using ECommerce.Shared.Abstractions.Exceptions;

namespace ECommerce.Modules.Orders.Application.Carts.Exceptions;

public sealed class CheckoutCartNotFoundException : ECommerceException
{
    public Guid UserId { get; }

    public CheckoutCartNotFoundException(Guid userId) : base($"Checkout cart for user with ID: {userId} does not exists.")
    {
        UserId = userId;
    }
}