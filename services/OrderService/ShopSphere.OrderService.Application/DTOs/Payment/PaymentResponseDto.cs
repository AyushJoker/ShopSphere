
using ShopSphere.OrderService.Application.Enums;

namespace ShopSphere.OrderService.Application.DTOs.Payment
{
    public class PaymentResponseDto
    {
        public Guid PaymentId { get; set; }

        public Guid OrderId { get; set; }

        public decimal Amount { get; set; }

        public PaymentStatus Status { get; set; }
    }
}
