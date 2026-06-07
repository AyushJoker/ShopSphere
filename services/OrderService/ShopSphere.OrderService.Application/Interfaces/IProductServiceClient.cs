public interface IProductServiceClient
{
    Task<ProductResponseDto?> GetProductAsync(Guid productId);
}