using ECommerce.Shared.Abstractions.Events;

namespace ECommerce.Modules.Orders.Application.Carts.Events.External;

public record ProductDiscountExpired(Guid ProductId) : IEvent;