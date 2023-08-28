using ECommerce.Shared.Abstractions.Events;

namespace ECommerce.Modules.Returns.Application.Events.External;

public record OrderPlaced(Guid Id, DateTime OrderPlace, IEnumerable<string> ProductSkus) : IEvent;