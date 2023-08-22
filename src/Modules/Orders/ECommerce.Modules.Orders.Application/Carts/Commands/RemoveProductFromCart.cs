using ECommerce.Shared.Abstractions.Commands;

namespace ECommerce.Modules.Orders.Application.Carts.Commands;

public record RemoveProductFromCart(Guid UserId, Guid ProductId) : ICommand;