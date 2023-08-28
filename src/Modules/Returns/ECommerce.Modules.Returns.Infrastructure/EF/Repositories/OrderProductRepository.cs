using ECommerce.Modules.Returns.Domain.Entities;
using ECommerce.Modules.Returns.Domain.Repositories;
using ECommerce.Shared.Abstractions.Kernel.Types;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Modules.Returns.Infrastructure.EF.Repositories;

internal sealed class OrderProductRepository : IOrderProductRepository
{
    private readonly ReturnsDbContext _context;

    public OrderProductRepository(ReturnsDbContext context)
    {
        _context = context;
    }

    public Task<OrderProduct> GetAsync(EntityId id)
        => _context.OrderProducts.SingleOrDefaultAsync(x => x.Id == id);

    public async Task AddAsync(OrderProduct orderProduct)
    {
        await _context.OrderProducts.AddAsync(orderProduct);
        await _context.SaveChangesAsync();
    }

    public async Task AddRangeAsync(IEnumerable<OrderProduct> orderProducts)
    {
        await _context.OrderProducts.AddRangeAsync(orderProducts);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(OrderProduct orderProduct)
    {
        _context.OrderProducts.Update(orderProduct);
        await _context.SaveChangesAsync();
    }
}