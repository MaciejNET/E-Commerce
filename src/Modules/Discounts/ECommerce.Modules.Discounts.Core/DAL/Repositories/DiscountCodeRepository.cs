using System.Collections;
using ECommerce.Modules.Discounts.Core.Entities;
using ECommerce.Modules.Discounts.Core.Repositories;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Modules.Discounts.Core.DAL.Repositories;

internal class DiscountCodeRepository : IDiscountCodeRepository
{
    private readonly DiscountsDbContext _context;

    public DiscountCodeRepository(DiscountsDbContext context)
    {
        _context = context;
    }

    public Task<bool> ExistsAsync(string code)
        => _context.DiscountCodes.AnyAsync(x => x.Code == code);

    public Task<DiscountCode> GetAsync(Guid id)
        => _context.DiscountCodes.SingleOrDefaultAsync(x => x.Id == id);

    public async Task<List<DiscountCode>> GetNewlyAddedDiscountCodesAsync(DateTime currentDate)
        => await _context.DiscountCodes
            .Where(x => x.ValidFrom > currentDate.AddMinutes(-1) && x.ValidFrom <= currentDate)
            .ToListAsync();

    public async Task<List<DiscountCode>> GetExpiredCodesAsync(DateTime currentDate)
        => await _context.DiscountCodes
            .Where(x => x.ValidTo >= currentDate.AddMinutes(-1) && x.ValidTo <= currentDate)
            .ToListAsync();

    public async Task AddAsync(DiscountCode discountCode)
    {
        await _context.DiscountCodes.AddAsync(discountCode);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(DiscountCode discountCode)
    {
        _context.DiscountCodes.Remove(discountCode);
        await _context.SaveChangesAsync();
    }
}