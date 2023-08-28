using ECommerce.Shared.Abstractions.Events;

namespace ECommerce.Modules.Returns.Domain.Events;

public record OrderReturned(Guid Id) : IEvent;