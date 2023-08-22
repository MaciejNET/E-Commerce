using ECommerce.Modules.Orders.Domain.Carts.Entities;
using ECommerce.Shared.Abstractions.Kernel.Types;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ECommerce.Modules.Orders.Infrastructure.EF.Configurations;

internal sealed class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasConversion(x => x.Value, v => new AggregateId(v));

        builder.HasIndex(x => x.Sku)
            .IsUnique();

        builder.Property(x => x.StandardPrice)
            .HasPrecision(18, 2);
        
        builder.Property(x => x.DiscountedPrice)
            .HasPrecision(18, 2);
    }
}