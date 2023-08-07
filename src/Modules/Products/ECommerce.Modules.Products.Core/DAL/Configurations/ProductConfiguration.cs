using ECommerce.Modules.Products.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ECommerce.Modules.Products.Core.DAL.Configurations;

internal sealed class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.Property(x => x.StandardPrice)
            .HasPrecision(18, 2);
        
        builder.Property(x => x.DiscountedPrice)
            .HasPrecision(18, 2);
    }
}