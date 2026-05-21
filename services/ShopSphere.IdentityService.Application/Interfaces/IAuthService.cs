using ShopSphere.IdentityService.Application.DTOs;

namespace ShopSphere.IdentityService.Application.Interfaces;

public interface IAuthService
{
    Task<AuthResponse> RegisterAsync(RegisterRequestDto request);

    Task<AuthResponse> LoginAsync(LoginRequestDto request);
    Task<AuthResponse> RefreshTokenAsync(RefreshTokenRequestDto request);
}