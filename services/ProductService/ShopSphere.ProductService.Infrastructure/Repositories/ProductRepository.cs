using Microsoft.EntityFrameworkCore;
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

    public async Task<List<Product>> GetAllAsync()
    {
        return await _context.Products
            .Include(p => p.Category)
            .ToListAsync();
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