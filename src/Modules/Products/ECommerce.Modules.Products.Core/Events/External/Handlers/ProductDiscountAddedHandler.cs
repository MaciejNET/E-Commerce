using ECommerce.Modules.Products.Core.Repositories;
using ECommerce.Shared.Abstractions.Events;
using Microsoft.Extensions.Logging;

namespace ECommerce.Modules.Products.Core.Events.External.Handlers;

internal class ProductDiscountAddedHandler : IEventHandler<ProductDiscountAdded>
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
            _logger.LogError("Product with ID: '{ProductId}' does not exists", @event.ProductId);
            return;
        }

        product.DiscountedPrice = @event.NewPrice;
        await _productRepository.UpdateAsync(product);
        _logger.LogInformation("Discount added for product with ID: '{ProductId}'", @event.ProductId);
    }
}