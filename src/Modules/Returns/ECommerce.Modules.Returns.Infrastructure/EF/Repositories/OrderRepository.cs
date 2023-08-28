using ECommerce.Modules.Returns.Domain.Entities;
using ECommerce.Modules.Returns.Domain.Repositories;
using ECommerce.Shared.Abstractions.Kernel.Types;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Modules.Returns.Infrastructure.EF.Repositories;

internal sealed class OrderRepository : IOrderRepository
{
    private readonly ReturnsDbContext _context;

    public OrderRepository(ReturnsDbContext context)
    {
        _context = context;
    }

    public Task<Order> GetAsync(AggregateId id)
        => _context.Orders
            .Include(x => x.Products)
            .SingleOrDefaultAsync(x => x.Id == id);

    public async Task<IEnumerable<Order>> BrowseAsync(UserId id)
        => await _context.Orders
            .Include(x => x.Products)
            .ToListAsync();

    public async Task AddAsync(Order order)
    {
        await _context.Orders.AddAsync(order);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Order order)
    {
        _context.Orders.Update(order);
        await _context.SaveChangesAsync();
    }
}