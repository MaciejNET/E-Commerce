using ECommerce.Shared.Abstractions.Exceptions;

namespace ECommerce.Modules.Orders.Application.Carts.Exceptions;

public sealed class CartNotCheckedOutException : ECommerceException
{
    public Guid Id { get; }

    public CartNotCheckedOutException(Guid id) : base($"User with ID: '{id}' does not have active checkout.")
    {
        Id = id;
    }
}