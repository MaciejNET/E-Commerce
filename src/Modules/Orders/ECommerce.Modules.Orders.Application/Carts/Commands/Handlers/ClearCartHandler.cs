using ECommerce.Modules.Orders.Application.Carts.Exceptions;
using ECommerce.Modules.Orders.Domain.Carts.Repositories;
using ECommerce.Shared.Abstractions.Commands;
using ECommerce.Shared.Abstractions.Kernel;
using ECommerce.Shared.Abstractions.Kernel.Types;
using ECommerce.Shared.Abstractions.Messaging;

namespace ECommerce.Modules.Orders.Application.Carts.Commands.Handlers;

public sealed class ClearCartHandler : ICommandHandler<ClearCart>
{
    private readonly ICartRepository _cartRepository;
    private readonly IMessageBroker _messageBroker;
    private readonly IDomainEventDispatcher _dispatcher;

    public ClearCartHandler(ICartRepository cartRepository, IMessageBroker messageBroker, IDomainEventDispatcher dispatcher)
    {
        _cartRepository = cartRepository;
        _messageBroker = messageBroker;
        _dispatcher = dispatcher;
    }

    public async Task HandleAsync(ClearCart command)
    {
        var cart = await _cartRepository.GetAsync(new UserId(command.UserId));

        if (cart is null)
        {
            throw new CartNotFoundException(command.UserId);
        }
        
        cart.Clear();
        await _cartRepository.UpdateAsync(cart);
        await _dispatcher.DispatchAsync(cart.Events.ToArray());
    }
}