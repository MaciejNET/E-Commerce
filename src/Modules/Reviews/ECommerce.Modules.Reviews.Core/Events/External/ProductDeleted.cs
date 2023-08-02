using ECommerce.Shared.Abstractions.Events;

namespace ECommerce.Modules.Reviews.Core.Events.External;

internal record ProductDeleted(Guid Id) : IEvent;