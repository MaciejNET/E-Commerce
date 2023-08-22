using ECommerce.Modules.Orders.Domain.Carts.Entities;
using ECommerce.Modules.Orders.Domain.Carts.Repositories;
using ECommerce.Shared.Abstractions.Kernel.Types;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Modules.Orders.Infrastructure.EF.Repositories;

internal sealed class CheckoutCartRepository : ICheckoutCartRepository
{
    private readonly OrdersDbContext _context;

    public CheckoutCartRepository(OrdersDbContext context)
    {
        _context = context;
    }

    public Task<CheckoutCart> GetAsync(AggregateId id)
        => _context.CheckoutCarts
            .Include(x => x.Items)
            .ThenInclude(x => x.Product)
            .SingleOrDefaultAsync(x => x.Id == id);

    public Task<CheckoutCart> GetAsync(UserId userId)
        => _context.CheckoutCarts
            .Include(x => x.Items)
            .ThenInclude(x => x.Product)
            .SingleOrDefaultAsync(x => x.UserId == userId);

    public async Task AddAsync(CheckoutCart checkoutCart)
    {
        await _context.CheckoutCarts.AddAsync(checkoutCart);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(CheckoutCart checkoutCart)
    {
        _context.CheckoutCarts.Update(checkoutCart);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(CheckoutCart checkoutCart)
    {
        _context.CheckoutCarts.Remove(checkoutCart);
        await _context.SaveChangesAsync();
    }
}