using ShopSphere.IdentityService.Application.DTOs;
using ShopSphere.IdentityService.Application.Interfaces;
using ShopSphere.IdentityService.Domain.Entities;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

namespace ShopSphere.IdentityService.Application.Services;

public class AuthService : IAuthService
{
    private readonly ILogger<AuthService> _logger;
    private readonly IUserRepository _userRepository;
    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    private readonly IRefreshTokenGenerator _refreshTokenGenerator;
    private readonly IRefreshTokenRepository _refreshTokenRepository;
    private readonly IDistributedCache _cache;
    public AuthService(
        IUserRepository userRepository,
        IJwtTokenGenerator jwtTokenGenerator,
        IRefreshTokenGenerator refreshTokenGenerator,
        IRefreshTokenRepository refreshTokenRepository,
        IDistributedCache cache,
         ILogger<AuthService> logger)
    {
        _userRepository = userRepository;
        _jwtTokenGenerator = jwtTokenGenerator;
        _refreshTokenGenerator = refreshTokenGenerator;
        _refreshTokenRepository = refreshTokenRepository;
        _cache = cache;
        _logger = logger;
    }

    public async Task<AuthResponse> RegisterAsync(RegisterRequestDto request)
    {
        request.Email = request.Email.Trim().ToLower();
        _logger.LogInformation("Register attempt for email: {Email}",request.Email);
        var existingUser =
            await _userRepository.GetUserByEmailAsync(request.Email);
      
        if (existingUser != null)
        {
            _logger.LogWarning("Registration failed. User already exists: {Email}",  request.Email);
            throw new ConflictException("User already exists");
        }

        var hashedPassword = BCrypt.Net.BCrypt.HashPassword(request.Password);

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

        return await GenerateAuthResponseAsync(user);

    }

    public async Task<AuthResponse> LoginAsync(LoginRequestDto request)
    {
        request.Email = request.Email.Trim().ToLower();
        _logger.LogInformation("Login attempt for email: {Email}", request.Email);
        var user =
            await _userRepository.GetUserByEmailAsync(request.Email);

        if (user == null)
        {
            _logger.LogWarning("Invalid login attempt for email: {Email}",request.Email);
             throw new UnauthorizedException("Invalid credentials");
        }

        var isPasswordValid =
            BCrypt.Net.BCrypt.Verify(
                request.Password,
                user.PasswordHash);

        if (!isPasswordValid)
        {
            _logger.LogWarning("Invalid login attempt for email: {Email}", request.Email);
            throw new UnauthorizedException("Invalid credentials");
        }

        _logger.LogInformation("User logged in successfully: {Email}",user.Email);

        return await GenerateAuthResponseAsync(user);
   
    }
    public async Task<UserProfileDto> GetProfileAsync(Guid userId)
    {
        var cacheKey = $"user_profile_{userId}";

        // STEP 1
        // Check Redis cache
        var cachedUser =
            await _cache.GetStringAsync(cacheKey);

        if (!string.IsNullOrEmpty(cachedUser))
        {
            _logger.LogInformation(
                "User profile fetched from Redis cache");

            return JsonSerializer.Deserialize<UserProfileDto>(
                cachedUser)!;
        }

        // STEP 2
        // Fetch from DB
        var user =
            await _userRepository.GetUserByIdAsync(userId);

        if (user == null)
        {
            throw new NotFoundException("User not found");
        }

        var userProfile = new UserProfileDto
        {
            Email = user.Email,

            FullName =
                $"{user.FirstName} {user.LastName}",

            Role = user.Role
        };

        // STEP 3
        // Store in Redis
        var cacheOptions =
            new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow =
                    TimeSpan.FromMinutes(5)
            };

        await _cache.SetStringAsync(
            cacheKey,
            JsonSerializer.Serialize(userProfile),
            cacheOptions);

        _logger.LogInformation(
            "User profile stored in Redis cache");

        return userProfile;
    }
    public async Task<AuthResponse> RefreshTokenAsync(RefreshTokenRequestDto request)
    {
        var existingRefreshToken =
            await _refreshTokenRepository
                .GetByTokenAsync(request.RefreshToken);

        if (existingRefreshToken == null)
        {
            throw new UnauthorizedException("Invalid refresh token");
        }

        if (existingRefreshToken.IsRevoked)
        {
            throw new UnauthorizedException("Refresh token revoked");
        }

        if (existingRefreshToken.ExpiryDate <= DateTime.UtcNow)
        {
            throw new UnauthorizedException("Refresh token expired");
        }

        var user =
            await _userRepository
                .GetUserByIdAsync(existingRefreshToken.UserId);

        if (user == null)
        {
            throw new NotFoundException("User not found");
        }

        // revoke old refresh token
        existingRefreshToken.IsRevoked = true;

        await _refreshTokenRepository.SaveChangesAsync();

        return await GenerateAuthResponseAsync(user);
    
    }


    private async Task<AuthResponse> GenerateAuthResponseAsync(User user)
    {
        var token =
            _jwtTokenGenerator.GenerateToken(
                user.Id,
                user.Email,
                user.FirstName,
                user.Role);

        var refreshToken =
            _refreshTokenGenerator.Generate();

        var refreshTokenEntity = new RefreshToken
        {
            Token = refreshToken,

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
            Token = token,

            RefreshToken = refreshToken,

            Email = user.Email,

            FullName =
                $"{user.FirstName} {user.LastName}"
        };
    }
    public async Task UpdateProfileAsync(Guid userId,UpdateProfileDto request)
    {
        var user =
            await _userRepository.GetUserByIdAsync(userId);

        if (user == null)
        {
            throw new NotFoundException("User not found");
        }

        user.FirstName = request.FirstName;

        user.LastName = request.LastName;

        await _userRepository.SaveChangesAsync();

        // REMOVE OLD CACHE
        var cacheKey = $"user_profile_{userId}";

        await _cache.RemoveAsync(cacheKey);

        _logger.LogInformation(
            "User profile cache invalidated for user: {UserId}",
            userId);
    }
}