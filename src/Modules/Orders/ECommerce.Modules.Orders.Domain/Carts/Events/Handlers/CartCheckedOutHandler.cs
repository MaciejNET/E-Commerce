using ECommerce.Modules.Orders.Domain.Carts.Entities;
using ECommerce.Modules.Orders.Domain.Carts.Repositories;
using ECommerce.Shared.Abstractions.Kernel;

namespace ECommerce.Modules.Orders.Domain.Carts.Events.Handlers;

public sealed class CartCheckedOutHandler : IDomainEventHandler<CartCheckedOut>
{
    private readonly ICheckoutCartRepository _checkoutCartRepository;

    public CartCheckedOutHandler(ICheckoutCartRepository checkoutCartRepository)
    {
        _checkoutCartRepository = checkoutCartRepository;
    }

    public async Task HandleAsync(CartCheckedOut @event)
    {
        var checkoutCart = new CheckoutCart(@event.Cart);

        await _checkoutCartRepository.AddAsync(checkoutCart);
    }
}