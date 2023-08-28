using ECommerce.Shared.Abstractions.Events;

namespace ECommerce.Modules.Orders.Application.Orders.Commands.External;

public record OrderReturned(Guid Id) : IEvent;