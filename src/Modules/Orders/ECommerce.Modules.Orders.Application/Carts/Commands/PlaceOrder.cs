using ECommerce.Shared.Abstractions.Commands;

namespace ECommerce.Modules.Orders.Application.Carts.Commands;

public record PlaceOrder(Guid UserId) : ICommand;