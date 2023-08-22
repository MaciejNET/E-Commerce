using ECommerce.Shared.Abstractions.Exceptions;

namespace ECommerce.Modules.Orders.Domain.Orders.Exceptions;

public sealed class InvalidOrderStatusChangeException : ECommerceException
{
    public string PreviousStatus { get; }
    public string NextStatus { get; }

    public InvalidOrderStatusChangeException(string previousStatus, string nextStatus) 
        : base($"Cannot change from '{previousStatus}' to '{nextStatus}'.")
    {
        PreviousStatus = previousStatus;
        NextStatus = nextStatus;
    }
}