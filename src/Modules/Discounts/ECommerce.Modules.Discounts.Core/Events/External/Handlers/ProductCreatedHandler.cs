using ECommerce.Modules.Discounts.Core.Entities;
using ECommerce.Modules.Discounts.Core.Repositories;
using ECommerce.Shared.Abstractions.Events;
using Microsoft.Extensions.Logging;

namespace ECommerce.Modules.Discounts.Core.Events.External.Handlers;

internal sealed class ProductCreatedHandler : IEventHandler<ProductCreated>
{
    private readonly IProductRepository _productRepository;
    private readonly ILogger<ProductCreatedHandler> _logger;

    public ProductCreatedHandler(IProductRepository productRepository, ILogger<ProductCreatedHandler> logger)
    {
        _productRepository = productRepository;
        _logger = logger;
    }

    public async Task HandleAsync(ProductCreated @event)
    {
        var product = new Product {Id = @event.Id};

        await _productRepository.AddAsync(product);
        _logger.LogInformation("Added a product with ID: \'{ProductId}\'", @event.Id);
    }
}