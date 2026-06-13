
using Microsoft.Extensions.Logging;
using ShopSphere.PaymentService.Application.Interfaces;
using ShopSphere.PaymentService.Domain.Entities;
using ShopSphere.PaymentService.Domain.Enums;

namespace ShopSphere.PaymentService.Application.Services;
public class PaymentService : IPaymentService
{
    private readonly IPaymentRepository
        _paymentRepository;

    private readonly ILogger<PaymentService>
        _logger;

    public PaymentService(
        IPaymentRepository paymentRepository,
        ILogger<PaymentService> logger)
    {
        _paymentRepository = paymentRepository;
        _logger = logger;
    }

    public async Task<PaymentResponseDto>CreatePaymentAsync(CreatePaymentRequestDto request)
    {
        var payment = new Payment
        {
            Id = Guid.NewGuid(),

            OrderId = request.OrderId,

            Amount = request.Amount,

            Status = request.SimulateFailure
    ? PaymentStatus.Failed
    : PaymentStatus.Success,

            CreatedAt = DateTime.UtcNow
        };

        await _paymentRepository
            .AddAsync(payment);

        await _paymentRepository
            .SaveChangesAsync();

        _logger.LogInformation(
            "Payment {PaymentId} created for Order {OrderId}",
            payment.Id,
            payment.OrderId);

        return new PaymentResponseDto
        {
            PaymentId = payment.Id,

            OrderId = payment.OrderId,

            Amount = payment.Amount,

            Status = payment.Status
        };
    }
}