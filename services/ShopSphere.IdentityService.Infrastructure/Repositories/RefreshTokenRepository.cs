using Microsoft.EntityFrameworkCore;
using ShopSphere.IdentityService.Application.Interfaces;
using ShopSphere.IdentityService.Domain.Entities;
using ShopSphere.IdentityService.Infrastructure.Data;

namespace ShopSphere.IdentityService.Infrastructure.Repositories;

public class RefreshTokenRepository
    : IRefreshTokenRepository
{
    private readonly IdentityDbContext _context;

    public RefreshTokenRepository(
        IdentityDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(
        RefreshToken refreshToken)
    {
        await _context.RefreshTokens
            .AddAsync(refreshToken);
    }

    public async Task<RefreshToken?> GetByTokenAsync(
        string token)
    {
        return await _context.RefreshTokens
            .FirstOrDefaultAsync(x => x.Token == token);
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}