using ECommerce.Modules.Orders.Domain.Carts.Entities;
using ECommerce.Shared.Abstractions.Kernel;

namespace ECommerce.Modules.Orders.Domain.Carts.Events;

public record CartCleared(Guid CartId, IEnumerable<CartItem> CartItems) : IDomainEvent;