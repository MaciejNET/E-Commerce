using ECommerce.Shared.Abstractions.Events;

namespace ECommerce.Modules.Products.Core.Events;

public record ProductUpdated(Guid Id, string Name, string Sku, decimal Price, int StockQuantity) : IEvent;