using ECommerce.Shared.Abstractions.Exceptions;

namespace ECommerce.Modules.Discounts.Core.Exceptions;

internal sealed class DiscountCodeNotFoundException : ECommerceException
{
    public Guid Id { get; }

    public DiscountCodeNotFoundException(Guid id) : base($"Discount code with ID: '{id}' was not found.")
    {
        Id = id;
    }
}