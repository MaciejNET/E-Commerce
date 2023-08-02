using ECommerce.Shared.Abstractions.Exceptions;

namespace ECommerce.Modules.Reviews.Core.Exceptions;

internal sealed class ReviewDeleteAuthorizationException : ECommerceException
{
    public Guid Id { get; }

    public ReviewDeleteAuthorizationException(Guid id) : base($"User is not authorized to delete review with ID: {id}")
    {
        Id = id;
    }
}