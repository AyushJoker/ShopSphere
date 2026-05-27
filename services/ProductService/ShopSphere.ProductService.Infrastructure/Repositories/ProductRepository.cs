using Microsoft.EntityFrameworkCore;
using ShopSphere.ProductService.Application.DTOs;
using ShopSphere.ProductService.Application.Interfaces;
using ShopSphere.ProductService.Domain.Entities;
using ShopSphere.ProductService.Infrastructure.Data;

namespace ShopSphere.ProductService.Infrastructure.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly ProductDbContext _context;

    public ProductRepository(ProductDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(Product product)
    {
        await _context.Products.AddAsync(product);
    }

    public async Task<Product?> GetByIdAsync(Guid id)
    {
        return await _context.Products
            .Include(p => p.Category)
            .FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task<(List<Product> Products, int TotalCount)> GetAllAsync(ProductQueryParameters parameters)
    {
        var query = _context.Products
            .Include(p => p.Category)
            .AsQueryable();

        if (!string.IsNullOrWhiteSpace(parameters.Search))
        {
            query = query.Where(p =>
                p.Name.Contains(parameters.Search));
        }

        if (parameters.CategoryId.HasValue)
        {
            query = query.Where(p =>
                p.CategoryId == parameters.CategoryId.Value);
        }

        query = parameters.SortBy?.ToLower() switch
        {
            "name" => parameters.Descending
                ? query.OrderByDescending(p => p.Name)
                : query.OrderBy(p => p.Name),

            "price" => parameters.Descending
                ? query.OrderByDescending(p => p.Price)
                : query.OrderBy(p => p.Price),

            "stock" => parameters.Descending
                ? query.OrderByDescending(p => p.StockQuantity)
                : query.OrderBy(p => p.StockQuantity),

            _ => query.OrderBy(p => p.Name)
        };

        var totalCount = await query.CountAsync();

        var products = await query
            .Skip((parameters.PageNumber - 1)
                * parameters.PageSize)
            .Take(parameters.PageSize)
            .ToListAsync();

        return (products, totalCount);
    }
    public async Task DeleteAsync(Product product)
    {
        _context.Products.Remove(product);

        await Task.CompletedTask;
    }
    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}