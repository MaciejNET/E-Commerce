using ECommerce.Shared.Abstractions.Exceptions;

namespace ECommerce.Modules.Users.Core.Exceptions
{
    internal class InvalidCredentialsException : ECommerceException
    {
        public InvalidCredentialsException() : base("Invalid credentials.")
        {
        }
    }
}