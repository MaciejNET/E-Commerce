using ECommerce.Shared.Abstractions.Events;

namespace ECommerce.Modules.Orders.Application.Carts.Events.External;

public record DiscountCodeAdded(Guid Id, string Code, int Percentage, List<Guid> ProductIds) : IEvent;