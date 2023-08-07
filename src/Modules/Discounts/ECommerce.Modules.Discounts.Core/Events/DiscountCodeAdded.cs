using ECommerce.Shared.Abstractions.Events;

namespace ECommerce.Modules.Discounts.Core.Events;

internal record DiscountCodeAdded(Guid Id, string Code, int Percentage, List<Guid> ProductIds) : IEvent;