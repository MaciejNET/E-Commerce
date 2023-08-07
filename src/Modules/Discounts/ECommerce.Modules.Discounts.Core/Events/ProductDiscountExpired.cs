using ECommerce.Shared.Abstractions.Events;

namespace ECommerce.Modules.Discounts.Core.Events;

internal record ProductDiscountExpired(Guid ProductId) : IEvent;