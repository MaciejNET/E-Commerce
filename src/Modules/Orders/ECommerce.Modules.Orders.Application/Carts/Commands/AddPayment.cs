using ECommerce.Modules.Orders.Domain.Shared.Enums;
using ECommerce.Shared.Abstractions.Commands;

namespace ECommerce.Modules.Orders.Application.Carts.Commands;

public record AddPayment(Guid UserId, PaymentMethod PaymentMethod) : ICommand;