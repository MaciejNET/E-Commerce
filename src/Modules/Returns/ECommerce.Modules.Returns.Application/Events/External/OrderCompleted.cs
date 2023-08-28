using ECommerce.Shared.Abstractions.Events;

namespace ECommerce.Modules.Returns.Application.Events.External;

public record OrderCompleted(Guid Id, DateTime Now) : IEvent;