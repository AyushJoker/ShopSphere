using ShopSphere.ProductService.Application.DTOs;
using ShopSphere.ProductService.Application.Interfaces;
using ShopSphere.ProductService.Domain.Entities;

namespace ShopSphere.ProductService.Application.Services;

public class ProductService : IProductService
{
    private readonly IProductRepository _productRepository;

    public ProductService(
        IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<ProductResponseDto> CreateAsync(
        CreateProductRequestDto request)
    {
        var product = new Product
        {
            Id = Guid.NewGuid(),
            Name = request.Name,
            Description = request.Description,
            Price = request.Price,
            StockQuantity = request.StockQuantity,
            CategoryId = request.CategoryId
        };

        await _productRepository.AddAsync(product);

        await _productRepository.SaveChangesAsync();

        return new ProductResponseDto
        {
            Id = product.Id,
            Name = product.Name,
            Description = product.Description,
            Price = product.Price,
            StockQuantity = product.StockQuantity,
            CategoryName = string.Empty
        };
    }
    public async Task<ProductResponseDto?> UpdateAsync(Guid id,UpdateProductRequestDto request)
    {
        var product =
            await _productRepository.GetByIdAsync(id);

        if (product == null)
        {
            return null;
        }

        product.Name = request.Name;

        product.Description = request.Description;

        product.Price = request.Price;

        product.StockQuantity = request.StockQuantity;

        product.CategoryId = request.CategoryId;

        await _productRepository.SaveChangesAsync();

        return new ProductResponseDto
        {
            Id = product.Id,
            Name = product.Name,
            Description = product.Description,
            Price = product.Price,
            StockQuantity = product.StockQuantity,
            CategoryName = product.Category.Name
        };
    }
    public async Task<bool> DeleteAsync(Guid id)
    {
        var product =
            await _productRepository.GetByIdAsync(id);

        if (product == null)
        {
            return false;
        }

        await _productRepository.DeleteAsync(product);

        await _productRepository.SaveChangesAsync();

        return true;
    }
    public async Task<ProductResponseDto?> GetByIdAsync(Guid id)
    {
        var product = await _productRepository
            .GetByIdAsync(id);

        if (product == null)
        {
            return null;
        }

        return new ProductResponseDto
        {
            Id = product.Id,
            Name = product.Name,
            Description = product.Description,
            Price = product.Price,
            StockQuantity = product.StockQuantity,
            CategoryName = product.Category.Name
        };
    }

    public async Task<List<ProductResponseDto>> GetAllAsync()
    {
        var products = await _productRepository
            .GetAllAsync();

        return products.Select(product =>
            new ProductResponseDto
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                StockQuantity = product.StockQuantity,
                CategoryName = product.Category.Name
            }).ToList();
    }
}