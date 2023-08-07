using ECommerce.Modules.Discounts.Core.Entities;

namespace ECommerce.Modules.Discounts.Core.Repositories;

internal interface IProductRepository
{
    Task<Product> GetAsync(Guid id);
    Task<IEnumerable<Product>> GetAsync(IEnumerable<Guid> ids);
    Task AddAsync(Product product);
    Task DeleteAsync(Product product);
}