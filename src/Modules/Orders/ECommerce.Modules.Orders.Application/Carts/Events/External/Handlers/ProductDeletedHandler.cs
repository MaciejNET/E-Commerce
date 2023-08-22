using ECommerce.Modules.Orders.Domain.Carts.Repositories;
using ECommerce.Shared.Abstractions.Events;
using Microsoft.Extensions.Logging;

namespace ECommerce.Modules.Orders.Application.Carts.Events.External.Handlers;

public class ProductDeletedHandler : IEventHandler<ProductDeleted>
{
    private readonly IProductRepository _productRepository;
    private readonly ILogger<ProductDeletedHandler> _logger;

    public ProductDeletedHandler(IProductRepository productRepository, ILogger<ProductDeletedHandler> logger)
    {
        _productRepository = productRepository;
        _logger = logger;
    }

    public async Task HandleAsync(ProductDeleted @event)
    {
        var product = await _productRepository.GetAsync(@event.Id);

        if (product is null)
        {
            _logger.LogError("Product with ID: '{ProductId}' was not found", @event.Id);
            return;
        }
        
        await _productRepository.DeleteAsync(product);
        _logger.LogInformation("Product with ID: '{ProductId}' has been deleted", @event.Id);
    }
}