using ECommerce.Shared.Abstractions.Exceptions;

namespace ECommerce.Modules.Orders.Application.Carts.Exceptions;

public sealed class CartAlreadyCheckedOutException : ECommerceException
{
    public Guid Id { get; }

    public CartAlreadyCheckedOutException(Guid id) : base($"User with ID: '{id}' already checkout cart.")
    {
        Id = id;
    }
}