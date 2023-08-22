using ECommerce.Shared.Abstractions.Commands;

namespace ECommerce.Modules.Orders.Application.Orders.Commands;

public record CancelOrder(Guid Id) : ICommand;