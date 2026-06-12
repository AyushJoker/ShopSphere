using System.Net;
using System.Net.Http.Json;
using ShopSphere.OrderService.Application.DTOs;
using ShopSphere.OrderService.Application.Interfaces;

public class InventoryServiceClient
    : IInventoryServiceClient
{
    private readonly HttpClient _httpClient;

    public InventoryServiceClient(
        HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task ReserveStockAsync(Guid productId,int quantity)
    {
        var request =
            new ReserveStockRequestDto
            {
                ProductId = productId,
                Quantity = quantity
            };

        try
        {
            var response =
                await _httpClient.PostAsJsonAsync(
                    "api/inventory/reserve",
                    request);

            if (response.StatusCode ==
                HttpStatusCode.BadRequest)
            {
                throw new InventoryReservationFailedException();
            }

            response.EnsureSuccessStatusCode();
        }
        catch (HttpRequestException)
        {
            throw new InventoryServiceUnavailableException();
        }
    }

    public async Task ReleaseStockAsync(Guid productId,int quantity)
    {
        var request =
            new ReleaseStockRequestDto
            {
                ProductId = productId,
                Quantity = quantity
            };

        try
        {
            var response =
                await _httpClient.PostAsJsonAsync(
                    "api/inventory/release",
                    request);

            response.EnsureSuccessStatusCode();
        }
        catch (HttpRequestException)
        {
            throw new InventoryServiceUnavailableException();
        }
    }
    public async Task DeductStockAsync(Guid productId,int quantity)
    {
        var request =
            new DeductStockRequestDto
            {
                ProductId = productId,
                Quantity = quantity
            };

        try
        {
            var response =
                await _httpClient.PostAsJsonAsync(
                    "api/inventory/deduct",
                    request);

            response.EnsureSuccessStatusCode();
        }
        catch (HttpRequestException)
        {
            throw new InventoryServiceUnavailableException();
        }
    }
}