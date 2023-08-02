using ECommerce.Modules.Reviews.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Modules.Reviews.Core.DAL;

internal class ReviewsDbContext : DbContext
{
    public DbSet<Review> Reviews { get; set; }
    public DbSet<Product> Products { get; set; }
    
    public ReviewsDbContext(DbContextOptions<ReviewsDbContext> options) : base(options)
    {
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("reviews");
        modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
    }
}