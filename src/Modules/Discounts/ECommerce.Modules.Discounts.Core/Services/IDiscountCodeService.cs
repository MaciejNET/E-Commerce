using ECommerce.Modules.Discounts.Core.DTO;

namespace ECommerce.Modules.Discounts.Core.Services;

internal interface IDiscountCodeService
{
    Task AddAsync(DiscountCodeDto dto);
    Task DeleteAsync(Guid id);
}