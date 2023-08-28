using ECommerce.Modules.Returns.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Modules.Returns.Infrastructure.EF;

internal sealed class ReturnsDbContext : DbContext
{
    public DbSet<Return> Returns { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderProduct> OrderProducts { get; set; }
    
    public ReturnsDbContext(DbContextOptions<ReturnsDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("returns");
        modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
    }
}