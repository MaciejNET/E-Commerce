using ECommerce.Modules.Orders.Application.Carts.Exceptions;
using ECommerce.Modules.Orders.Domain.Carts.Repositories;
using ECommerce.Shared.Abstractions.Commands;
using ECommerce.Shared.Abstractions.Kernel.Types;

namespace ECommerce.Modules.Orders.Application.Carts.Commands.Handlers;

public sealed class AddProductToCartHandler : ICommandHandler<AddProductToCart>
{
    private readonly ICartRepository _cartRepository;
    private readonly IProductRepository _productRepository;

    public AddProductToCartHandler(ICartRepository cartRepository, IProductRepository productRepository)
    {
        _cartRepository = cartRepository;
        _productRepository = productRepository;
    }

    public async Task HandleAsync(AddProductToCart command)
    {
        var cart = await _cartRepository.GetAsync(new UserId(command.UserId));

        if (cart is null)
        {
            throw new CartNotFoundException(command.UserId);
        }

        var product = await _productRepository.GetAsync(command.ProductId);

        if (product is null)
        {
            throw new ProductNotFoundException(command.ProductId);
        }
        
        cart.AddItem(product, command.Quantity);

        await _cartRepository.UpdateAsync(cart);
    }
}