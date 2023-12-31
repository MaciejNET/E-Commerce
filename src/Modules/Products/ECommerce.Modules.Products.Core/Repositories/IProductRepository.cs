using ECommerce.Modules.Products.Core.Entities;

namespace ECommerce.Modules.Products.Core.Repositories;

internal interface IProductRepository
{
    Task<Product> GetAsync(Guid id);
    Task<bool> ExistsAsync(string sku);
    Task<IEnumerable<Product>> BrowseAsync(string? searchText, Guid? categoryId, decimal? minPrice, decimal? maxPrice);
    Task AddAsync(Product product);
    Task UpdateAsync(Product product);
}