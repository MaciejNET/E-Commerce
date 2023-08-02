using ECommerce.Shared.Abstractions.Exceptions;

namespace ECommerce.Modules.Reviews.Core.Exceptions;

internal class ReviewExistsException : ECommerceException
{
    public Guid UserId { get; }
    public string Email { get; }

    public ReviewExistsException(string email) : base($"User with email: '{email} already placed review.'")
    {
        Email = email;
    }
    
    public ReviewExistsException(Guid userId) : base($"User with ID: '{userId} already placed review.'")
    {
        UserId = userId;
    }
}