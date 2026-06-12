using ShopSphere.PaymentService.Domain.Enums;

public class PaymentResponseDto
{
    public Guid PaymentId { get; set; }

    public Guid OrderId { get; set; }

    public decimal Amount { get; set; }

    public PaymentStatus Status { get; set; }
}