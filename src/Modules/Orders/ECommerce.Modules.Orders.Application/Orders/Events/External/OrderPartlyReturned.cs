using ECommerce.Shared.Abstractions.Events;

namespace ECommerce.Modules.Orders.Application.Orders.Events.External;

public record OrderPartlyReturned(Guid Id) : IEvent;