using ECommerce.Shared.Abstractions.Exceptions;

namespace ECommerce.Modules.Orders.Domain.Carts.Exceptions;

public sealed class InvalidItemQuantityException : ECommerceException
{
    public InvalidItemQuantityException() 
        : base("Cart item quantity must be equal or grater that 0.")
    {
    }
}