using ECommerce.Modules.Products.Core.DTO;
using ECommerce.Modules.Products.Core.Entities;
using ECommerce.Modules.Products.Core.Events;
using ECommerce.Modules.Products.Core.Exceptions;
using ECommerce.Modules.Products.Core.Repositories;
using ECommerce.Shared.Abstractions.Messaging;

namespace ECommerce.Modules.Products.Core.Services;

internal class ProductService : IProductService
{
    private readonly IProductRepository _productRepository;
    private readonly ICategoryRepository _categoryRepository;
    private readonly IMessageBroker _messageBroker;

    public ProductService(IProductRepository productRepository, ICategoryRepository categoryRepository, IMessageBroker messageBroker)
    {
        _productRepository = productRepository;
        _categoryRepository = categoryRepository;
        _messageBroker = messageBroker;
    }

    public async Task<ProductDto> GetAsync(Guid id)
        => Map(await _productRepository.GetAsync(id));

    public async Task<IEnumerable<ProductDto>> BrowseAsync(string searchText, Guid? categoryId, decimal? minPrice, decimal? maxPrice)
    {
        var products = await _productRepository.BrowseAsync(searchText, categoryId, minPrice, maxPrice);

        return products.Select(Map);
    }

    public async Task AddAsync(ProductDetailsDto dto)
    {
        var exists = await _productRepository.ExistsAsync(dto.Sku);

        if (exists)
        {
            throw new ProductAlreadyExistsException(dto.Sku);
        }

        var category = await _categoryRepository.GetAsync(dto.Category);

        if (category is null)
        {
            throw new CategoryNotFoundException(dto.Name);
        }

        dto.Id = Guid.NewGuid();
        var product = new Product
        {
            Id = dto.Id,
            CategoryId = category.Id,
            Category = category,
            Name = dto.Name,
            Manufacturer = dto.Manufacturer,
            Description = dto.Description,
            Sku = dto.Sku,
            StandardPrice = dto.Price,
            StockQuantity = dto.StockQuantity,
            ImageUrl = dto.ImageUrl
        };

        await _productRepository.AddAsync(product);
        await _messageBroker.PublishAsync(new ProductCreated(product.Id, product.Name, product.Sku, product.StandardPrice, product.StockQuantity));
    }

    public async Task UpdateAsync(ProductDetailsDto dto)
    {
        var product = await _productRepository.GetAsync(dto.Id);

        if (product is null)
        {
            throw new ProductNotFoundException(dto.Id);
        }

        product.Name = dto.Name;
        product.Manufacturer = dto.Manufacturer;
        product.Description = dto.Description;
        product.Sku = dto.Sku;
        product.StandardPrice = dto.Price;
        product.StockQuantity = dto.StockQuantity;
        product.ImageUrl = dto.ImageUrl;

        var category = await _categoryRepository.GetAsync(dto.Category);

        if (category is null)
        {
            throw new CategoryNotFoundException(dto.Category);
        }
        
        product.Category = category;
        product.CategoryId = category.Id;

        await _productRepository.UpdateAsync(product);
        await _messageBroker.PublishAsync(new ProductUpdated(product.Id, product.Name, product.Sku, product.StandardPrice, product.StockQuantity));
    }

    public async Task DeleteAsync(Guid id)
    {
        var product = await _productRepository.GetAsync(id);

        if (product is null)
        {
            throw new ProductNotFoundException(id);
        }

        product.IsDeleted = true;
        await _productRepository.UpdateAsync(product);
        await _messageBroker.PublishAsync(new ProductDeleted(product.Id));
    }

    private static ProductDto Map(Product product)
        => new()
        {
            Id = product.Id,
            Category = product.Category.Name,
            Name = product.Name,
            Manufacturer = product.Manufacturer,
            Description = product.Description,
            Sku = product.Sku,
            StandardPrice = product.StandardPrice,
            DiscountedPrice = product.DiscountedPrice,
            ImageUrl = product.ImageUrl,
            IsAvailable = product.IsAvailable
        };
}