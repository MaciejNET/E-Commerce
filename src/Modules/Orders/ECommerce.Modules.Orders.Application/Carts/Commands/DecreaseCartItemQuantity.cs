using ECommerce.Shared.Abstractions.Commands;

namespace ECommerce.Modules.Orders.Application.Carts.Commands;

public record DecreaseCartItemQuantity(Guid Id, int Quantity) : ICommand;