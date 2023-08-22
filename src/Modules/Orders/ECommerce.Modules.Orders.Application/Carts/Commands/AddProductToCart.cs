using ECommerce.Shared.Abstractions.Commands;

namespace ECommerce.Modules.Orders.Application.Carts.Commands;

public record AddProductToCart(Guid UserId, Guid ProductId, int Quantity) : ICommand;