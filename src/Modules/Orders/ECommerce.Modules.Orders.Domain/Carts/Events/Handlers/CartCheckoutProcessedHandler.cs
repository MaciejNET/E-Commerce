using ECommerce.Modules.Orders.Domain.Carts.Repositories;
using ECommerce.Shared.Abstractions.Kernel;
using ECommerce.Shared.Abstractions.Kernel.Types;

namespace ECommerce.Modules.Orders.Domain.Carts.Events.Handlers;

public sealed class CartCheckoutProcessedHandler : IDomainEventHandler<CartCheckoutProcessed>
{
    private readonly ICartRepository _cartRepository;
    private readonly ICheckoutCartRepository _checkoutCartRepository;
    private readonly IDomainEventDispatcher _dispatcher;

    public CartCheckoutProcessedHandler(ICartRepository cartRepository, ICheckoutCartRepository checkoutCartRepository, IDomainEventDispatcher dispatcher)
    {
        _cartRepository = cartRepository;
        _checkoutCartRepository = checkoutCartRepository;
        _dispatcher = dispatcher;
    }

    public async Task HandleAsync(CartCheckoutProcessed @event)
    {
        var cart = await _cartRepository.GetAsync(new UserId(@event.UserId));
        var checkoutCart = await _checkoutCartRepository.GetAsync(new UserId(@event.UserId));

        await _checkoutCartRepository.DeleteAsync(checkoutCart);
        cart.Clear();
        await _dispatcher.DispatchAsync(cart.Events.ToArray());
    }
}