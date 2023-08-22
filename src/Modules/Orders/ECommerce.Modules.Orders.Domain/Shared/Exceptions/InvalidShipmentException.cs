using ECommerce.Shared.Abstractions.Exceptions;

namespace ECommerce.Modules.Orders.Domain.Shared.Exceptions;

public sealed class InvalidShipmentException : ECommerceException
{
    public string Property { get; }

    public InvalidShipmentException(string property) : base($"{property} cannot be null or empty.")
    {
        Property = property;
    }
}