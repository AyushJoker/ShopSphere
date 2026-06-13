using ShopSphere.PaymentService.Domain.Entities;
namespace ShopSphere.PaymentService.Application.Interfaces;
public interface IPaymentRepository
{
    Task AddAsync(Payment payment);

    Task<Payment?> GetByIdAsync(Guid id);

    Task SaveChangesAsync();
}