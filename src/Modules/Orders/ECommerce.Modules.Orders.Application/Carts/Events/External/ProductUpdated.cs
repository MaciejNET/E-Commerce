using ECommerce.Shared.Abstractions.Events;

namespace ECommerce.Modules.Orders.Application.Carts.Events.External;

public record ProductUpdated(Guid Id, string Name, string Sku, decimal Price, int StockQuantity) : IEvent;