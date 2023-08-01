using ECommerce.Modules.Products.Core.Entities;

namespace ECommerce.Modules.Products.Core.Repositories;

internal interface ICategoryRepository
{
    Task<bool> ExistsByNameAsync(string name);
    Task<Category> GetAsync(string name);
    Task<IEnumerable<Category>> BrowseAsync();
    Task AddAsync(Category category);
}