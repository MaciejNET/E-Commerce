using ECommerce.Shared.Abstractions.Exceptions;

namespace ECommerce.Modules.Reviews.Core.Exceptions;

internal class ReviewNotFoundException : ECommerceException
{
    public Guid Id { get; }

    public ReviewNotFoundException(Guid id) : base($"Review with ID: '{id}' does not exists.")
    {
        Id = id;
    }
}