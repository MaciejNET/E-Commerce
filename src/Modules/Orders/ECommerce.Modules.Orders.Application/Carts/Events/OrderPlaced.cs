using ECommerce.Shared.Abstractions.Events;

namespace ECommerce.Modules.Orders.Application.Carts.Events;

public record OrderPlaced(Guid Id, DateTime OrderPlace, IEnumerable<string> ProductSkus) : IEvent;