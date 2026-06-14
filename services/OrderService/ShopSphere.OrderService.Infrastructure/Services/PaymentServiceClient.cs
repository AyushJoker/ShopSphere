using System.Net.Http.Json;
using ShopSphere.OrderService.Application.DTOs.Payment;
using ShopSphere.OrderService.Application.Interfaces;

namespace ShopSphere.OrderService.Infrastructure.Services;

public class PaymentServiceClient
    : IPaymentServiceClient
{
    private readonly HttpClient _httpClient;

    public PaymentServiceClient(
        HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<PaymentResponseDto> CreatePaymentAsync(CreatePaymentRequestDto request)
    {
        try
        {
            var response =
                await _httpClient.PostAsJsonAsync(
                    "api/payment",
                    request);

            response.EnsureSuccessStatusCode();

            return await response.Content
                       .ReadFromJsonAsync<PaymentResponseDto>()
                   ?? throw new Exception(
                       "Payment response was null.");
        }
        catch (HttpRequestException)
        {
            throw new PaymentServiceUnavailableException();
        }
    }
}