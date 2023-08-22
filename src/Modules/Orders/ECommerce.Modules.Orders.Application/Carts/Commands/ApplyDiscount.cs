using ECommerce.Shared.Abstractions.Commands;

namespace ECommerce.Modules.Orders.Application.Carts.Commands;

public record ApplyDiscount(Guid UserId, string Code) : ICommand;