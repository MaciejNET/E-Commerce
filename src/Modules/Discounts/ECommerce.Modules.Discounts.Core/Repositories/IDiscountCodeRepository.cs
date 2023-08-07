using ECommerce.Modules.Discounts.Core.Entities;

namespace ECommerce.Modules.Discounts.Core.Repositories;

internal interface IDiscountCodeRepository
{
    Task<bool> ExistsAsync(string code);
    Task<DiscountCode> GetAsync(Guid id);
    Task<List<DiscountCode>> GetExpiredCodesAsync(DateTime currentDate);
    Task AddAsync(DiscountCode discountCode);
    Task DeleteAsync(DiscountCode discountCode);
}