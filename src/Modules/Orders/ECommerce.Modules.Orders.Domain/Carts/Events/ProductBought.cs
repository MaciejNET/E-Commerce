using ECommerce.Shared.Abstractions.Kernel;

namespace ECommerce.Modules.Orders.Domain.Carts.Events;

public record ProductBought(Guid Id, int Quantity) : IDomainEvent;