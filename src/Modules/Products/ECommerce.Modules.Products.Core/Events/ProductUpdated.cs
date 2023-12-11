using ECommerce.Shared.Abstractions.Events;
using ECommerce.Shared.Abstractions.Kernel.Types;

namespace ECommerce.Modules.Products.Core.Events;

public record ProductUpdated(Guid Id, string Name, string Sku, Price Price, int StockQuantity) : IEvent;