using ECommerce.Shared.Abstractions.Events;
using ECommerce.Shared.Abstractions.Kernel;

namespace ECommerce.Modules.Products.Core.Events.External;

public record ProductBought(Guid ProductId, int Quantity) : IEvent, IDomainEvent;