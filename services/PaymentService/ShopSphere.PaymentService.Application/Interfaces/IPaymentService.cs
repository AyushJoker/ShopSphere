public interface IPaymentService
{
    Task<PaymentResponseDto>
        CreatePaymentAsync(
            CreatePaymentRequestDto request);
}