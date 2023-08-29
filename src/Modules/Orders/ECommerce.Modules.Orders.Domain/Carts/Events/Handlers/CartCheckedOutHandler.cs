using ECommerce.Modules.Orders.Domain.Carts.Entities;
using ECommerce.Modules.Orders.Domain.Carts.Repositories;
using ECommerce.Shared.Abstractions.Kernel;

namespace ECommerce.Modules.Orders.Domain.Carts.Events.Handlers;

public sealed class CartCheckedOutHandler : IDomainEventHandler<CartCheckedOut>
{
    private readonly ICheckoutCartRepository _checkoutCartRepository;
    private readonly ICartItemRepository _cartItemRepository;

    public CartCheckedOutHandler(ICheckoutCartRepository checkoutCartRepository, ICartItemRepository cartItemRepository)
    {
        _checkoutCartRepository = checkoutCartRepository;
        _cartItemRepository = cartItemRepository;
    }

    public async Task HandleAsync(CartCheckedOut @event)
    {
        var checkoutCart = new CheckoutCart(@event.Cart);

        await _checkoutCartRepository.AddAsync(checkoutCart);
        await _cartItemRepository.UpdateRangeAsync(checkoutCart.Items);
    }
}