using ECommerce.Modules.Orders.Domain.Carts.Entities;
using ECommerce.Modules.Orders.Domain.Carts.Repositories;
using ECommerce.Shared.Abstractions.Kernel;

namespace ECommerce.Modules.Orders.Domain.Carts.Events.Handlers;

public sealed class CartCheckedOutHandler : IDomainEventHandler<CartCheckedOut>
{
    private readonly ICheckoutCartRepository _checkoutCartRepository;
    private readonly ICheckoutCartItemRepository _checkoutCartItemRepository;

    public CartCheckedOutHandler(ICheckoutCartRepository checkoutCartRepository, ICheckoutCartItemRepository checkoutCartItemRepository)
    {
        _checkoutCartRepository = checkoutCartRepository;
        _checkoutCartItemRepository = checkoutCartItemRepository;
    }

    public async Task HandleAsync(CartCheckedOut @event)
    {
        var checkoutCart = new CheckoutCart(@event.Cart, @event.Items);

        await _checkoutCartRepository.AddAsync(checkoutCart);
        await _checkoutCartItemRepository.AddRangeAsync(checkoutCart.Items);
    }
}