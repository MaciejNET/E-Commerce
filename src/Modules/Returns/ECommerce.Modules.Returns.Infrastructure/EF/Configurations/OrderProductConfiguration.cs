using ECommerce.Modules.Returns.Domain.Entities;
using ECommerce.Shared.Abstractions.Kernel.Types;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ECommerce.Modules.Returns.Infrastructure.EF.Configurations;

internal sealed class OrderProductConfiguration : IEntityTypeConfiguration<OrderProduct>
{
    public void Configure(EntityTypeBuilder<OrderProduct> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasConversion(x => x.Value, v => new EntityId(v));

        builder.Property(x => x.OrderId)
            .HasConversion(x => x.Value, v => new AggregateId(v));
    }
}