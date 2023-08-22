using ECommerce.Modules.Orders.Domain.Carts.Exceptions;
using ECommerce.Modules.Orders.Domain.Carts.Repositories;
using ECommerce.Shared.Abstractions.Commands;
using ECommerce.Shared.Abstractions.Kernel.Types;

namespace ECommerce.Modules.Orders.Application.Carts.Commands.Handlers;

public sealed class IncreaseCartItemQuantityHandler : ICommandHandler<IncreaseCartItemQuantity>
{
    private readonly ICartItemRepository _cartItemRepository;

    public IncreaseCartItemQuantityHandler(ICartItemRepository cartItemRepository)
    {
        _cartItemRepository = cartItemRepository;
    }

    public async Task HandleAsync(IncreaseCartItemQuantity command)
    {
        var cartItem = await _cartItemRepository.GetAsync(new EntityId(command.Id));

        if (cartItem is null)
        {
            throw new CartItemNotFoundException(command.Id);
        }
        
        cartItem.IncreaseQuantity(command.Quantity);
        await _cartItemRepository.UpdateAsync(cartItem);
    }
}