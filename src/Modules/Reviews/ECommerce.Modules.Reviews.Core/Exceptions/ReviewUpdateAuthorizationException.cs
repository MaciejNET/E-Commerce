using ECommerce.Shared.Abstractions.Exceptions;

namespace ECommerce.Modules.Reviews.Core.Exceptions;

internal sealed class ReviewUpdateAuthorizationException : ECommerceException
{
    public Guid Id { get; }

    public ReviewUpdateAuthorizationException(Guid id) : base($"User is not authorized to update review with ID: {id}")
    {
        Id = id;
    }
}