using ECommerce.Modules.Reviews.Core.Entities;
using ECommerce.Modules.Reviews.Core.Repositories;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Modules.Reviews.Core.DAL.Repositories;

internal class ProductRepository : IProductRepository
{
    private readonly ReviewsDbContext _context;

    public ProductRepository(ReviewsDbContext context)
    {
        _context = context;
    }

    public Task<bool> ExistsAsync(Guid id)
        => _context.Products.AnyAsync(x => x.Id == id);

    public async Task AddAsync(Product product)
    {
        await _context.Products.AddAsync(product);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Product product)
    {
        _context.Products.Remove(product);
        await _context.SaveChangesAsync();
    }
}