using ShopSphere.PaymentService.Domain.Entities;

public interface IPaymentRepository
{
    Task AddAsync(Payment payment);

    Task<Payment?> GetByIdAsync(Guid id);

    Task SaveChangesAsync();
}