using ShopSphere.ProductService.Application.DTOs;
using ShopSphere.ProductService.Domain.Entities;

namespace ShopSphere.ProductService.Application.Interfaces;

public interface IProductService
{
    Task<ProductResponseDto> CreateAsync(
        CreateProductRequestDto request);

    Task<ProductResponseDto?> GetByIdAsync(Guid id);

    Task<PagedResponse<ProductResponseDto>> GetAllAsync(ProductQueryParameters parameters);
    Task<ProductResponseDto?> UpdateAsync(Guid id,UpdateProductRequestDto request);

    Task<bool> DeleteAsync(Guid id);
}