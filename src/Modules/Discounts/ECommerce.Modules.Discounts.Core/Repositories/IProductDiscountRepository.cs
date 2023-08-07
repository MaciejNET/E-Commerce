using ECommerce.Modules.Discounts.Core.Entities;

namespace ECommerce.Modules.Discounts.Core.Repositories;

internal interface IProductDiscountRepository
{
    Task<bool> CanAddDiscountForProductAsync(Guid productId, DateTime from, DateTime to);
    Task<ProductDiscount> GetAsync(Guid id);
    Task<List<ProductDiscount>> GetExpiredProductsAsync(DateTime currentDate);
    Task AddAsync(ProductDiscount productDiscount);
    Task DeleteAsync(ProductDiscount productDiscount);
}