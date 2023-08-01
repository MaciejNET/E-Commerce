using ECommerce.Modules.Products.Core.DTO;

namespace ECommerce.Modules.Products.Core.Services;

internal interface ICategoryService
{
    Task<IEnumerable<CategoryDetailsDto>> BrowseAsync();
    Task AddAsync(CategoryDto dto);
}