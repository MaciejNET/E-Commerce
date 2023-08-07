using ECommerce.Shared.Abstractions.Exceptions;

namespace ECommerce.Modules.Discounts.Core.Exceptions;

internal sealed class ProductNotFoundException : ECommerceException
{
    public Guid Id { get; }

    public ProductNotFoundException(Guid id) : base($"Product with ID: '{id}' was not found.")
    {
        Id = id;
    }
}