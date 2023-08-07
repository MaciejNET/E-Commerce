using ECommerce.Shared.Abstractions.Exceptions;

namespace ECommerce.Modules.Discounts.Core.Exceptions;

internal sealed class ProductDiscountNotFoundException : ECommerceException
{
    public Guid Id { get; }

    public ProductDiscountNotFoundException(Guid id) : base($"Product discount with ID: '{id}' was not found.")
    {
        Id = id;
    }
}