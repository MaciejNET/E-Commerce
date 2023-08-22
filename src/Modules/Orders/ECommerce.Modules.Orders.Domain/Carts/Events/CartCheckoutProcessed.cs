using ECommerce.Shared.Abstractions.Kernel;

namespace ECommerce.Modules.Orders.Domain.Carts.Events;

public record CartCheckoutProcessed(Guid UserId) : IDomainEvent;