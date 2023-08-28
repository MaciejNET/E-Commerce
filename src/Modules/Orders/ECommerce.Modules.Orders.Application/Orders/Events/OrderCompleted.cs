using ECommerce.Shared.Abstractions.Events;

namespace ECommerce.Modules.Orders.Application.Orders.Events;

public record OrderCompleted(Guid Id, DateTime Now) : IEvent;