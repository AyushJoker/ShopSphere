using ShopSphere.OrderService.Application.DTOs.Payment;

namespace ShopSphere.OrderService.Application.Interfaces
{
    public interface IPaymentServiceClient
    {
        Task<PaymentResponseDto>
            CreatePaymentAsync(
                CreatePaymentRequestDto request);
    }
}
