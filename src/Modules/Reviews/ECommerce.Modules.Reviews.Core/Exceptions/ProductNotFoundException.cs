using ECommerce.Shared.Abstractions.Exceptions;

namespace ECommerce.Modules.Reviews.Core.Exceptions;

internal class ProductNotFoundException : ECommerceException
{
    public Guid Id { get; }

    public ProductNotFoundException(Guid id) : base($"Product with ID: '{id}' does not exists.")
    {
        Id = id;
    }
}