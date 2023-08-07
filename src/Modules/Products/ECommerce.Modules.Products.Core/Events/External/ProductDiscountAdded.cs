using ECommerce.Shared.Abstractions.Events;

namespace ECommerce.Modules.Products.Core.Events.External;

public record ProductDiscountAdded(Guid ProductId, decimal NewPrice) : IEvent;