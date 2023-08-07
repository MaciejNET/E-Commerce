using ECommerce.Shared.Abstractions.Events;

namespace ECommerce.Modules.Discounts.Core.Events;

internal record ProductDiscountAdded(Guid ProductId, decimal NewPrice) : IEvent;