using ECommerce.Shared.Abstractions.Commands;

namespace ECommerce.Modules.Returns.Application.Commands;

public record DeclineReturn(Guid Id) : ICommand;