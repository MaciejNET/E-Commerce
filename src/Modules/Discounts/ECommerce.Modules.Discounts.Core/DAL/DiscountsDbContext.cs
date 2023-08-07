using ECommerce.Modules.Discounts.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Modules.Discounts.Core.DAL;

internal class DiscountsDbContext : DbContext
{
    public DbSet<DiscountCode> DiscountCodes { get; set; }
    public DbSet<ProductDiscount> ProductDiscounts { get; set; }
    public DbSet<Product> Products { get; set; }

    public DiscountsDbContext(DbContextOptions<DiscountsDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("discounts");
        modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
    }
}