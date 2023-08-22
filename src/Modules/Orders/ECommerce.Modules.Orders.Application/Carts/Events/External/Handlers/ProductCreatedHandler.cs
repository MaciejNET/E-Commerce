using ECommerce.Modules.Orders.Domain.Carts.Entities;
using ECommerce.Modules.Orders.Domain.Carts.Repositories;
using ECommerce.Shared.Abstractions.Events;
using Microsoft.Extensions.Logging;

namespace ECommerce.Modules.Orders.Application.Carts.Events.External.Handlers;

public class ProductCreatedHandler : IEventHandler<ProductCreated>
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
        var product = await _productRepository.GetAsync(@event.Id);

        if (product is not null)
        {
            _logger.LogError("Product with ID: '{ProductId}' already exists", product.Id.ToString());
            return;
        }
        
        product = new Product(@event.Id, @event.Name, @event.Sku, @event.Price, @event.StockQuantity);

        await _productRepository.AddAsync(product);
        _logger.LogInformation("Product with ID: {ProductId} has been added", @event.Id);
    }
}