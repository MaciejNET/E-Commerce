using ECommerce.Shared.Abstractions.Events;

namespace ECommerce.Modules.Orders.Application.Orders.Events;

public record OrderSent(Guid Id) : IEvent;