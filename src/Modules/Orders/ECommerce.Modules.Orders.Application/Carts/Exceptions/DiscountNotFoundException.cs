using ECommerce.Shared.Abstractions.Exceptions;

namespace ECommerce.Modules.Orders.Application.Carts.Exceptions;

public sealed class DiscountNotFoundException : ECommerceException
{
    public string Code { get; }

    public DiscountNotFoundException(string code) : base($"Discount with code: '{code}' was not found.")
    {
        Code = code;
    }
}