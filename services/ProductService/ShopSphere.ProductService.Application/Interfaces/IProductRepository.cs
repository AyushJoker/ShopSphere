using ShopSphere.ProductService.Application.DTOs;
using ShopSphere.ProductService.Domain.Entities;

namespace ShopSphere.ProductService.Application.Interfaces;

public interface IProductRepository
{
    Task AddAsync(Product product);

    Task<Product?> GetByIdAsync(Guid id);

    Task<(List<Product> Products, int TotalCount)> GetAllAsync(ProductQueryParameters parameters);
    Task DeleteAsync(Product product);


    Task SaveChangesAsync();
}