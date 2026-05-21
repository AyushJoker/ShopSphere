using BCrypt.Net;
using ShopSphere.IdentityService.Application.DTOs;
using ShopSphere.IdentityService.Application.Interfaces;
using ShopSphere.IdentityService.Domain.Entities;
using Microsoft.Extensions.Logging;

namespace ShopSphere.IdentityService.Application.Services;

public class AuthService : IAuthService
{
    private readonly ILogger<AuthService> _logger;
    private readonly IUserRepository _userRepository;
    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    private readonly IRefreshTokenGenerator _refreshTokenGenerator;
    private readonly IRefreshTokenRepository _refreshTokenRepository;
    public AuthService(
        IUserRepository userRepository,
        IJwtTokenGenerator jwtTokenGenerator,
        IRefreshTokenGenerator refreshTokenGenerator,
        IRefreshTokenRepository refreshTokenRepository,
         ILogger<AuthService> logger)
    {
        _userRepository = userRepository;
        _jwtTokenGenerator = jwtTokenGenerator;
        _refreshTokenGenerator = refreshTokenGenerator;
        _refreshTokenRepository = refreshTokenRepository;
        _logger = logger;
    }

    public async Task<AuthResponse> RegisterAsync(RegisterRequestDto request)
    {
        _logger.LogInformation("Register attempt for email: {Email}",request.Email);
        var existingUser =
            await _userRepository.GetUserByEmailAsync(request.Email);
      
        if (existingUser != null)
        {
            _logger.LogWarning("Registration failed. User already exists: {Email}",  request.Email);
            throw new Exception("User already exists");
        }

        var hashedPassword =
             BCrypt.Net.BCrypt.HashPassword(request.Password);

        var user = new User
        {
            Id = Guid.NewGuid(),
            FirstName = request.FirstName,
            LastName = request.LastName,
            Email = request.Email,
            PasswordHash = hashedPassword
            
        };

        await _userRepository.AddUserAsync(user);

        await _userRepository.SaveChangesAsync();

        _logger.LogInformation("User registered successfully: {Email}", user.Email);

        var token = _jwtTokenGenerator.GenerateToken(user.Id, user.Email,user.FirstName,user.Role);

        var refreshToken = _refreshTokenGenerator.Generate();

        var refreshTokenEntity = new RefreshToken
        {
            Token = refreshToken,

            ExpiryDate = DateTime.UtcNow.AddDays(7),

            UserId = user.Id,

            IsRevoked = false
        };
        await _refreshTokenRepository.AddAsync(refreshTokenEntity);

        await _refreshTokenRepository.SaveChangesAsync();

        return new AuthResponse
        {
            Token = token,

            RefreshToken = refreshToken,

            Email = user.Email,

            FullName = $"{user.FirstName} {user.LastName}"
        };

    }

    public async Task<AuthResponse> LoginAsync(LoginRequestDto request)
    {
        _logger.LogInformation("Login attempt for email: {Email}", request.Email);
        var user =
            await _userRepository.GetUserByEmailAsync(request.Email);

        if (user == null)
        {
            _logger.LogWarning("Invalid login attempt for email: {Email}",request.Email);
            throw new Exception("Invalid credentials");
        }

        var isPasswordValid =
            BCrypt.Net.BCrypt.Verify(
                request.Password,
                user.PasswordHash);

        if (!isPasswordValid)
        {
            _logger.LogWarning("Invalid login attempt for email: {Email}", request.Email);
            throw new Exception("Invalid credentials");
        }
        _logger.LogInformation("User logged in successfully: {Email}",user.Email);

        var token = _jwtTokenGenerator.GenerateToken(user.Id, user.Email, user.FirstName, user.Role);

        var refreshToken = _refreshTokenGenerator.Generate();

        var refreshTokenEntity = new RefreshToken
        {
            Token = refreshToken,

            ExpiryDate = DateTime.UtcNow.AddDays(7),

            UserId = user.Id,

            IsRevoked = false
        };

        await _refreshTokenRepository.AddAsync(refreshTokenEntity);

        await _refreshTokenRepository.SaveChangesAsync();

        return new AuthResponse
        {
            Token = token,

            RefreshToken = refreshToken,

            Email = user.Email,

            FullName = $"{user.FirstName} {user.LastName}"
        };
    }
    public async Task<AuthResponse> RefreshTokenAsync(RefreshTokenRequestDto request)
    {
        var existingRefreshToken =
            await _refreshTokenRepository
                .GetByTokenAsync(request.RefreshToken);

        if (existingRefreshToken == null)
        {
            throw new Exception("Invalid refresh token");
        }

        if (existingRefreshToken.IsRevoked)
        {
            throw new Exception("Refresh token revoked");
        }

        if (existingRefreshToken.ExpiryDate <= DateTime.UtcNow)
        {
            throw new Exception("Refresh token expired");
        }

        var user =
            await _userRepository
                .GetUserByIdAsync(existingRefreshToken.UserId);

        if (user == null)
        {
            throw new Exception("User not found");
        }

        // revoke old refresh token
        existingRefreshToken.IsRevoked = true;

        // generate new access token
        var newAccessToken =
            _jwtTokenGenerator.GenerateToken(
                user.Id,
                user.Email,
                user.FirstName,
                user.Role);

        // generate new refresh token
        var newRefreshToken =
            _refreshTokenGenerator.Generate();

        var refreshTokenEntity = new RefreshToken
        {
            Token = newRefreshToken,

            ExpiryDate = DateTime.UtcNow.AddDays(7),

            UserId = user.Id,

            IsRevoked = false
        };

        await _refreshTokenRepository
            .AddAsync(refreshTokenEntity);

        await _refreshTokenRepository
            .SaveChangesAsync();

        return new AuthResponse
        {
            Token = newAccessToken,

            RefreshToken = newRefreshToken,

            Email = user.Email,

            FullName =
                $"{user.FirstName} {user.LastName}"
        };
    }
}