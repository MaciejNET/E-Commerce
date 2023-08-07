using ECommerce.Shared.Abstractions.Exceptions;

namespace ECommerce.Modules.Discounts.Core.Exceptions;

internal sealed class InvalidDiscountDateException : ECommerceException
{
    public InvalidDiscountDateException() : base("Invalid discount date.")
    {
    }
}