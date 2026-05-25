using ShopSphere.IdentityService.Domain.Entities;

namespace ShopSphere.IdentityService.Application.Interfaces;

public interface IRefreshTokenRepository
{
    Task AddAsync(RefreshToken refreshToken);

    Task<RefreshToken?> GetByTokenAsync(string token);

    Task SaveChangesAsync();
}