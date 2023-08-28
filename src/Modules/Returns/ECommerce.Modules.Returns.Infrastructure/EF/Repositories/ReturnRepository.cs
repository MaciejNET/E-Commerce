using ECommerce.Modules.Returns.Domain.Entities;
using ECommerce.Modules.Returns.Domain.Repositories;
using ECommerce.Shared.Abstractions.Kernel.Types;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Modules.Returns.Infrastructure.EF.Repositories;

internal sealed class ReturnRepository : IReturnRepository
{
    private readonly ReturnsDbContext _context;

    public ReturnRepository(ReturnsDbContext context)
    {
        _context = context;
    }

    public Task<Return> GetAsync(AggregateId id)
        => _context.Returns
            .Include(x => x.OrderProduct)
            .Include(x => x.Order)
            .SingleOrDefaultAsync(x => x.Id == id);

    public async Task<IEnumerable<Return>> BrowsAsync()
        => await _context.Returns
            .Include(x => x.OrderProduct)
            .Include(x => x.Order)
            .ToListAsync();

    public async Task AddAsync(Return @return)
    {
        await _context.Returns.AddAsync(@return);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Return @return)
    {
        _context.Returns.Update(@return);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Return @return)
    {
        _context.Returns.Remove(@return);
        await _context.SaveChangesAsync();
    }
}