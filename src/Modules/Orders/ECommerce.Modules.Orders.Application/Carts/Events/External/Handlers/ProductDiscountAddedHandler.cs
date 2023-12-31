using ECommerce.Modules.Orders.Domain.Carts.Repositories;
using ECommerce.Shared.Abstractions.Events;
using Microsoft.Extensions.Logging;

namespace ECommerce.Modules.Orders.Application.Carts.Events.External.Handlers;

public class ProductDiscountAddedHandler : IEventHandler<ProductDiscountAdded>
{
    private readonly IProductRepository _productRepository;
    private readonly ILogger<ProductDiscountAddedHandler> _logger;

    public ProductDiscountAddedHandler(IProductRepository productRepository, ILogger<ProductDiscountAddedHandler> logger)
    {
        _productRepository = productRepository;
        _logger = logger;
    }

    public async Task HandleAsync(ProductDiscountAdded @event)
    {
        var product = await _productRepository.GetAsync(@event.ProductId);

        if (product is null)
        {
            _logger.LogError("Product with ID: '{ProductId}' was not found", @event.ProductId.ToString());
            return;
        }
        
        product.SetDiscountedPrice(@event.NewPrice);
        await _productRepository.UpdateAsync(product);
        _logger.LogInformation("Added discounted price for product with ID: '{ProductId}'", product.Id.ToString());
    }
}