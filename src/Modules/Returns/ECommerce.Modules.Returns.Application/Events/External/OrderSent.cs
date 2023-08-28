using ECommerce.Shared.Abstractions.Events;

namespace ECommerce.Modules.Returns.Application.Events.External;

public record OrderSent(Guid Id) : IEvent;