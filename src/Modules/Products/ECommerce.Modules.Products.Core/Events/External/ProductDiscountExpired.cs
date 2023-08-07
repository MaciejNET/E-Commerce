using ECommerce.Shared.Abstractions.Events;

namespace ECommerce.Modules.Products.Core.Events.External;

public record ProductDiscountExpired(Guid ProductId) : IEvent;