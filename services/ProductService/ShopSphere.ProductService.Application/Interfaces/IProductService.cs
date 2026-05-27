using ShopSphere.ProductService.Application.DTOs;

namespace ShopSphere.ProductService.Application.Interfaces;

public interface IProductService
{
    Task<ProductResponseDto> CreateAsync(
        CreateProductRequestDto request);

    Task<ProductResponseDto?> GetByIdAsync(Guid id);

    Task<List<ProductResponseDto>> GetAllAsync();
    Task<ProductResponseDto?> UpdateAsync(Guid id,UpdateProductRequestDto request);

    Task<bool> DeleteAsync(Guid id);
}