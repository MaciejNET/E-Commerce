using ECommerce.Shared.Abstractions.Exceptions;

namespace ECommerce.Modules.Orders.Domain.Carts.Exceptions;

public sealed class InvalidProductStockQuantityException : ECommerceException
{
    public InvalidProductStockQuantityException() 
        : base("Stock quantity must be equal or grater that 0.")
    {
    }
}