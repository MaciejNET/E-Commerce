using ECommerce.Modules.Returns.Domain.Entities;
using ECommerce.Shared.Abstractions.Kernel;

namespace ECommerce.Modules.Returns.Domain.Events;

public record ReturnStatusChanged(Guid OrderId, Guid OrderProductId, ReturnStatus Status) : IDomainEvent;