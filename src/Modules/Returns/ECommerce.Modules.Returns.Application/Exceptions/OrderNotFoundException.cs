using ECommerce.Shared.Abstractions.Exceptions;

namespace ECommerce.Modules.Returns.Application.Exceptions;

public sealed class OrderNotFoundException : ECommerceException
{
    public Guid Id { get; }

    public OrderNotFoundException(Guid id) : base($"Order with ID: '{id}' does not exists.")
    {
        Id = id;
    }
}