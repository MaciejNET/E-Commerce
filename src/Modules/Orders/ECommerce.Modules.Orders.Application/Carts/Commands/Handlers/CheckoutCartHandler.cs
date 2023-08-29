using ECommerce.Modules.Orders.Application.Carts.Exceptions;
using ECommerce.Modules.Orders.Domain.Carts.Repositories;
using ECommerce.Shared.Abstractions.Commands;
using ECommerce.Shared.Abstractions.Kernel;
using ECommerce.Shared.Abstractions.Kernel.Types;

namespace ECommerce.Modules.Orders.Application.Carts.Commands.Handlers;

public sealed class CheckoutCartHandler : ICommandHandler<CheckoutCart>
{
    private readonly ICartRepository _cartRepository;
    private readonly ICheckoutCartRepository _checkoutCartRepository;
    private readonly IDomainEventDispatcher _dispatcher;

    public CheckoutCartHandler(ICartRepository cartRepository, ICheckoutCartRepository checkoutCartRepository, IDomainEventDispatcher dispatcher)
    {
        _cartRepository = cartRepository;
        _checkoutCartRepository = checkoutCartRepository;
        _dispatcher = dispatcher;
    }

    public async Task HandleAsync(CheckoutCart command)
    {
        var cart = await _cartRepository.GetAsync(new UserId(command.UserId));

        if (cart is null)
        {
            throw new CartNotFoundException(command.UserId);
        }
        
        var checkoutCart = await _checkoutCartRepository.GetAsync(new UserId(command.UserId));

        if (checkoutCart is not null)
        {
            throw new CartAlreadyCheckedOutException(command.UserId);
        }

        cart.Checkout();
        await _dispatcher.DispatchAsync(cart.Events.ToArray());
    }
}