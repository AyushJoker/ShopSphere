using ShopSphere.ProductService.Application.DTOs;
using ShopSphere.ProductService.Application.Interfaces;
using ShopSphere.ProductService.Domain.Entities;

namespace ShopSphere.ProductService.Application.Services;

public class ProductService : IProductService
{
    private readonly IProductRepository _productRepository;
    private readonly ICacheService _cacheService;
    public ProductService(IProductRepository productRepository, ICacheService cacheService)
    {
        _productRepository = productRepository;
        _cacheService = cacheService;
    }

    public async Task<ProductResponseDto> CreateAsync(CreateProductRequestDto request)
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

        await _cacheService.RemoveByPrefixAsync("products:");

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
            throw new NotFoundException("Product not found.");
        }

        product.Name = request.Name;

        product.Description = request.Description;

        product.Price = request.Price;

        product.StockQuantity = request.StockQuantity;

        product.CategoryId = request.CategoryId;

        await _productRepository.SaveChangesAsync();

        await _cacheService.RemoveByPrefixAsync("products:");

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
            throw new NotFoundException("Product not found.");
        }

        await _productRepository.DeleteAsync(product);

        await _productRepository.SaveChangesAsync();

        await _cacheService.RemoveByPrefixAsync("products:");

        return true;
    }
    public async Task<ProductResponseDto?> GetByIdAsync(Guid id)
    {
        var product = await _productRepository
            .GetByIdAsync(id);

        if (product == null)
        {
            throw new NotFoundException("Product not found");
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

    public async Task<PagedResponse<ProductResponseDto>> GetAllAsync(ProductQueryParameters parameters)
    {
        var cacheKey =
         $"products:" +
         $"{parameters.PageNumber}:" +
         $"{parameters.PageSize}:" +
         $"{parameters.Search ?? "none"}:" +
         $"{parameters.CategoryId?.ToString() ?? "none"}:" +
         $"{parameters.SortBy ?? "none"}:" +
         $"{parameters.Descending}";

        var cachedProducts = await _cacheService.GetAsync<PagedResponse<ProductResponseDto>>(cacheKey);

        if (cachedProducts is not null)
        {
            return cachedProducts;
        }

        var (products, totalCount) =
            await _productRepository
                .GetAllAsync(parameters);

        var items = products.Select(product =>
            new ProductResponseDto
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                StockQuantity = product.StockQuantity,
                CategoryName = product.Category.Name
            }).ToList();

      

        var response= new PagedResponse<ProductResponseDto>
        {
            Items = items,
            PageNumber = parameters.PageNumber,
            PageSize = parameters.PageSize,
            TotalCount = totalCount
        };
        await _cacheService.SetAsync(cacheKey, response, TimeSpan.FromMinutes(5));
        return response;
    }
}