namespace ShopSphere.PaymentService.Application.Interfaces;
public interface IPaymentService
{
    Task<PaymentResponseDto>
        CreatePaymentAsync(
            CreatePaymentRequestDto request);
}