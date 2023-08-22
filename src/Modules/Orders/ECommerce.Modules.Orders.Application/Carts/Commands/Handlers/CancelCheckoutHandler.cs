using ECommerce.Modules.Orders.Application.Carts.Exceptions;
using ECommerce.Modules.Orders.Domain.Carts.Repositories;
using ECommerce.Shared.Abstractions.Commands;
using ECommerce.Shared.Abstractions.Kernel.Types;

namespace ECommerce.Modules.Orders.Application.Carts.Commands.Handlers;

public sealed class CancelCheckoutHandler : ICommandHandler<CancelCheckout>
{
    private readonly ICheckoutCartRepository _checkoutCartRepository;

    public CancelCheckoutHandler(ICheckoutCartRepository checkoutCartRepository)
    {
        _checkoutCartRepository = checkoutCartRepository;
    }

    public async Task HandleAsync(CancelCheckout command)
    {
        var checkoutCart = await _checkoutCartRepository.GetAsync(new UserId(command.UserId));

        if (checkoutCart is null)
        {
            throw new CartNotCheckedOutException(command.UserId);
        }

        await _checkoutCartRepository.DeleteAsync(checkoutCart);
    }
}