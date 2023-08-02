using ECommerce.Modules.Reviews.Core.Entities;
using ECommerce.Modules.Reviews.Core.Repositories;
using ECommerce.Shared.Abstractions.Events;
using Microsoft.Extensions.Logging;

namespace ECommerce.Modules.Reviews.Core.Events.External.Handlers;

internal sealed class ProductDeletedHandler : IEventHandler<ProductDeleted>
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
        var product = new Product {Id = @event.Id};

        await _productRepository.DeleteAsync(product);
        _logger.LogInformation("Deleted a product with ID: \'{ProductId}\'", @event.Id);
    }
}