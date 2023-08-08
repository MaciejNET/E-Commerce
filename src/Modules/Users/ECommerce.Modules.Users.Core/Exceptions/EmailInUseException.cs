using ECommerce.Shared.Abstractions.Exceptions;

namespace ECommerce.Modules.Users.Core.Exceptions
{
    internal class EmailInUseException : ECommerceException
    {
        public EmailInUseException() : base("Email is already in use.")
        {
        }
    }
}