using ECommerce.Shared.Abstractions.Exceptions;

namespace ECommerce.Modules.Discounts.Core.Exceptions;

internal sealed class DiscountCodeAlreadyExistsException : ECommerceException
{
    public string Code { get; }

    public DiscountCodeAlreadyExistsException(string code) : base($"Discount code: '{code}' already exists.")
    {
        Code = code;
    }
}