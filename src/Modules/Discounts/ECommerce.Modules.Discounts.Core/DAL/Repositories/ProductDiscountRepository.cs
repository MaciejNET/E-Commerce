using ECommerce.Modules.Discounts.Core.Entities;
using ECommerce.Modules.Discounts.Core.Repositories;
using ECommerce.Shared.Abstractions.Time;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Modules.Discounts.Core.DAL.Repositories;

internal class ProductDiscountRepository : IProductDiscountRepository
{
    private readonly DiscountsDbContext _context;
    private readonly IClock _clock;

    public ProductDiscountRepository(DiscountsDbContext context, IClock clock)
    {
        _context = context;
        _clock = clock;
    }

    public async Task<bool> CanAddDiscountForProductAsync(Guid productId, DateTime from, DateTime to)
    {
        var productDiscounts = await _context.ProductDiscounts
            .Where(x => x.ProductId == productId && x.ValidFrom.Date > _clock.CurrentDate())
            .ToListAsync();

        var noOverlappingProjects = productDiscounts
            .Where(x => x.ValidFrom > to || x.ValidTo < from);

        var overlappingDiscounts = productDiscounts.Except(noOverlappingProjects);

        return !overlappingDiscounts.Any();
    }

    public Task<ProductDiscount> GetAsync(Guid id)
        => _context.ProductDiscounts.SingleOrDefaultAsync(x => x.Id == id);

    public async Task<List<ProductDiscount>> GetExpiredProductsAsync(DateTime currentDate)
        => await _context.ProductDiscounts
            .Where(x => x.ValidTo >= currentDate.AddMinutes(-1) && x.ValidTo <= currentDate)
            .ToListAsync();

    public async Task AddAsync(ProductDiscount productDiscount)
    {
        await _context.ProductDiscounts.AddAsync(productDiscount);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(ProductDiscount productDiscount)
    {
        _context.ProductDiscounts.Remove(productDiscount);
        await _context.SaveChangesAsync();
    }
}