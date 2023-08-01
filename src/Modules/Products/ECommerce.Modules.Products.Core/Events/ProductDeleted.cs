using ECommerce.Shared.Abstractions.Events;

namespace ECommerce.Modules.Products.Core.Events;

public record ProductDeleted(Guid Id) : IEvent;