using System.Net.Http.Json;

public class ProductServiceClient
    : IProductServiceClient
{
    private readonly HttpClient _httpClient;

    public ProductServiceClient(
        HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<ProductResponseDto?> GetProductAsync( Guid productId)
    {
        try
        {
            var response =
                await _httpClient.GetAsync(
                    $"api/products/{productId}");

            if (response.StatusCode ==
                System.Net.HttpStatusCode.NotFound)
            {
                return null;
            }

            response.EnsureSuccessStatusCode();

            return await response.Content
                .ReadFromJsonAsync<ProductResponseDto>();
        }
        catch (HttpRequestException)
        {
            throw new ProductServiceUnavailableException();
        }
    }
}