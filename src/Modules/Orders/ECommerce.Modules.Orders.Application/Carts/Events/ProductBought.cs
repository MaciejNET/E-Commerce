using ECommerce.Shared.Abstractions.Events;

namespace ECommerce.Modules.Orders.Application.Carts.Events;

public record ProductBought(Guid ProductId, int Quantity) : IEvent;