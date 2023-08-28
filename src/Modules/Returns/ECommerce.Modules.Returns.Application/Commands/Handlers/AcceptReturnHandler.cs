using ECommerce.Modules.Returns.Application.Exceptions;
using ECommerce.Modules.Returns.Domain.Entities;
using ECommerce.Modules.Returns.Domain.Repositories;
using ECommerce.Shared.Abstractions.Commands;

namespace ECommerce.Modules.Returns.Application.Commands.Handlers;

internal sealed class AcceptReturnHandler : ICommandHandler<AcceptReturn>
{
    private readonly IReturnRepository _returnRepository;

    public AcceptReturnHandler(IReturnRepository returnRepository)
    {
        _returnRepository = returnRepository;
    }

    public async Task HandleAsync(AcceptReturn command)
    {
        var @return = await _returnRepository.GetAsync(command.Id);

        if (@return is null)
        {
            throw new ReturnNotFoundException(command.Id);
        }
        
        @return.ChangeStatus(ReturnStatus.Accepted);
        await _returnRepository.UpdateAsync(@return);
    }
}