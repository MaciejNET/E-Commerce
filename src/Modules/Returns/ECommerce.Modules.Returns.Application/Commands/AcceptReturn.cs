using ECommerce.Shared.Abstractions.Commands;

namespace ECommerce.Modules.Returns.Application.Commands;

public record AcceptReturn(Guid Id) : ICommand;