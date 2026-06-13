using Microsoft.EntityFrameworkCore;
using ShopSphere.PaymentService.Application.Interfaces;
using ShopSphere.PaymentService.Domain.Entities;
using ShopSphere.PaymentService.Infrastructure.Data;

namespace ShopSphere.PaymentService.Infrastructure.Repositories;
public class PaymentRepository : IPaymentRepository
{
    private readonly PaymentDbContext _context;

    public PaymentRepository(
        PaymentDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(Payment payment)
    {
        await _context.Payments.AddAsync(payment);
    }

    public async Task<Payment?> GetByIdAsync(Guid id)
    {
        return await _context.Payments
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}