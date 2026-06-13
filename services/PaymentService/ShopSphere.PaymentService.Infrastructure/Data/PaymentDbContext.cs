using Microsoft.EntityFrameworkCore;
using ShopSphere.PaymentService.Domain.Entities;

namespace ShopSphere.PaymentService.Infrastructure.Data;

public class PaymentDbContext : DbContext
{
    public PaymentDbContext(
        DbContextOptions<PaymentDbContext> options)
        : base(options)
    {
    }

    public DbSet<Payment> Payments { get; set; }
}