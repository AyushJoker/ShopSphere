using Microsoft.EntityFrameworkCore;
using ShopSphere.IdentityService.Domain.Entities;

namespace ShopSphere.IdentityService.Infrastructure.Data;

public class IdentityDbContext : DbContext
{
    public IdentityDbContext(DbContextOptions<IdentityDbContext> options)
        : base(options)
    {
    }

    public DbSet<User> Users => Set<User>();
}