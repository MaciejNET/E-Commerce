using ECommerce.Shared.Abstractions.Exceptions;

namespace ECommerce.Modules.Returns.Application.Exceptions;

public sealed class ReturnNotFoundException : ECommerceException
{
    public Guid Id { get; }

    public ReturnNotFoundException(Guid id) : base($"Return with ID: '{id}' does not exists.")
    {
        Id = id;
    }
}