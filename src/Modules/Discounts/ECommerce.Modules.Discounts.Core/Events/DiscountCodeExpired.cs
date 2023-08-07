using ECommerce.Shared.Abstractions.Events;

namespace ECommerce.Modules.Discounts.Core.Events;

internal record DiscountCodeExpired(Guid Id) : IEvent;