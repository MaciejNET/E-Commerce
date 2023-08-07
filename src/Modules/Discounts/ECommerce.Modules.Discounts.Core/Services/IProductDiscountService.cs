using ECommerce.Modules.Discounts.Core.DTO;

namespace ECommerce.Modules.Discounts.Core.Services;

internal interface IProductDiscountService
{
    Task AddAsync(ProductDiscountDto dto);
    Task DeleteAsync(Guid id);
}