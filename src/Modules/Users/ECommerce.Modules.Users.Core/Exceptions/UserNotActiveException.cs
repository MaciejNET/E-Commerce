using ECommerce.Shared.Abstractions.Exceptions;

namespace ECommerce.Modules.Users.Core.Exceptions
{
    internal class UserNotActiveException : ECommerceException
    {
        public Guid UserId { get; }

        public UserNotActiveException(Guid userId) : base($"User with ID: '{userId}' is not active.")
        {
            UserId = userId;
        }
    }
}