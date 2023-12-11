using ECommerce.Shared.Abstractions.Events;
using ECommerce.Shared.Abstractions.Kernel.Types;

namespace ECommerce.Modules.Orders.Application.Carts.Events.External;

public record ProductDiscountAdded(Guid ProductId, Price NewPrice) : IEvent;