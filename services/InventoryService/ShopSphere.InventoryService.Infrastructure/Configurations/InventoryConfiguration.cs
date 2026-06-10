using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShopSphere.InventoryService.Domain.Entities;

namespace ShopSphere.InventoryService.Infrastructure.Configurations;

public class InventoryConfiguration
    : IEntityTypeConfiguration<Inventory>
{
    public void Configure(
        EntityTypeBuilder<Inventory> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.ProductId)
            .IsRequired();

        builder.Property(x => x.AvailableQuantity)
            .IsRequired();

        builder.Property(x => x.ReservedQuantity)
            .IsRequired();

        builder.Property(x => x.LastUpdated)
            .IsRequired();
    }
}