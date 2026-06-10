using Microsoft.EntityFrameworkCore;
using ShopSphere.InventoryService.Domain.Entities;

namespace ShopSphere.InventoryService.Infrastructure.Data;

public class InventoryDbContext : DbContext
{
    public InventoryDbContext(
        DbContextOptions<InventoryDbContext> options)
        : base(options)
    {
    }

    public DbSet<Inventory> Inventories =>
        Set<Inventory>();

    protected override void OnModelCreating(
        ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(
            typeof(InventoryDbContext).Assembly);

        base.OnModelCreating(modelBuilder);
    }
}