using ECommerce.Modules.Reviews.Core.Entities;

namespace ECommerce.Modules.Reviews.Core.Repositories;

internal interface IProductRepository
{
    Task<bool> ExistsAsync(Guid id);
    Task AddAsync(Product product);
    Task DeleteAsync(Product product);
}