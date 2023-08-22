using ECommerce.Shared.Abstractions.Commands;

namespace ECommerce.Modules.Orders.Application.Carts.Commands;

public record IncreaseCartItemQuantity(Guid Id, int Quantity) : ICommand;