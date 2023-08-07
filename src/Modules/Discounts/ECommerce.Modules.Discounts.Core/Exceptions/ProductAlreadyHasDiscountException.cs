using ECommerce.Shared.Abstractions.Exceptions;

namespace ECommerce.Modules.Discounts.Core.Exceptions;

internal sealed class ProductAlreadyHasDiscountException : ECommerceException
{
    public Guid Id { get; }

    public ProductAlreadyHasDiscountException(Guid id) : base($"Product with ID: '{id}' already has discount in given date.")
    {
        Id = id;
    }
}