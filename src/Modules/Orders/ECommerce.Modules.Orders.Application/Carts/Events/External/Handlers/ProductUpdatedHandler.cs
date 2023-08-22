using ECommerce.Modules.Orders.Domain.Carts.Repositories;
using ECommerce.Shared.Abstractions.Events;
using Microsoft.Extensions.Logging;

namespace ECommerce.Modules.Orders.Application.Carts.Events.External.Handlers;

public class ProductUpdatedHandler : IEventHandler<ProductUpdated>
{
    private readonly IProductRepository _productRepository;
    private readonly ILogger<ProductUpdatedHandler> _logger;

    public ProductUpdatedHandler(IProductRepository productRepository, ILogger<ProductUpdatedHandler> logger)
    {
        _productRepository = productRepository;
        _logger = logger;
    }

    public async Task HandleAsync(ProductUpdated @event)
    {
        var product = await _productRepository.GetAsync(@event.Id);

        if (product is null)
        {
            _logger.LogError("Product with ID: '{ProductId}' was not found", @event.Id);
            return;
        }
        
        product.SetPrice(@event.Price);
        product.SetStockQuantity(@event.StockQuantity);

        await _productRepository.UpdateAsync(product);
        _logger.LogInformation("Product with ID: '{ProductId}' has been updated", @event.Id);
    }
}