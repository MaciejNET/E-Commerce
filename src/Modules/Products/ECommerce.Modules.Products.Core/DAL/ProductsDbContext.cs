using ECommerce.Modules.Products.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Modules.Products.Core.DAL;

internal class ProductsDbContext : DbContext
{
    public DbSet<Product> Products { get; set; }
    public DbSet<Category> Categories { get; set; }

    public ProductsDbContext(DbContextOptions<ProductsDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("products");
        modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
    }
}