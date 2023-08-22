using ECommerce.Shared.Abstractions.Exceptions;

namespace ECommerce.Modules.Orders.Domain.Orders.Exceptions;

public sealed class InvalidOrderLineException : ECommerceException
{
    public string Property { get; }

    public InvalidOrderLineException(string property) : base($"Invalid {property} in order line.")
    {
        Property = property;
    }
}