using ECommerce.Shared.Abstractions.Events;
using ECommerce.Shared.Abstractions.Kernel.Types;

namespace ECommerce.Modules.Products.Core.Events.External;

public record ProductDiscountAdded(Guid ProductId, Price NewPrice) : IEvent;