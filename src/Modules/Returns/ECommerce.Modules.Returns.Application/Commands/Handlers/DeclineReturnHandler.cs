using ECommerce.Modules.Returns.Application.Exceptions;
using ECommerce.Modules.Returns.Domain.Entities;
using ECommerce.Modules.Returns.Domain.Repositories;
using ECommerce.Shared.Abstractions.Commands;

namespace ECommerce.Modules.Returns.Application.Commands.Handlers;

internal sealed class DeclineReturnHandler : ICommandHandler<DeclineReturn>
{
    private readonly IReturnRepository _returnRepository;

    public DeclineReturnHandler(IReturnRepository returnRepository)
    {
        _returnRepository = returnRepository;
    }

    public async Task HandleAsync(DeclineReturn command)
    {
        var @return = await _returnRepository.GetAsync(command.Id);

        if (@return is null)
        {
            throw new ReturnNotFoundException(command.Id);
        }
        
        @return.ChangeStatus(ReturnStatus.Declined);
        await _returnRepository.UpdateAsync(@return);
    }
}