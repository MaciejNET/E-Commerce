using ECommerce.Shared.Abstractions.Events;

namespace ECommerce.Modules.Discounts.Core.Events.External;

internal record ProductDeleted(Guid Id) : IEvent;