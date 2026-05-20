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

    public AuthService(
        IUserRepository userRepository,
        IJwtTokenGenerator jwtTokenGenerator,
         ILogger<AuthService> logger)
    {
        _userRepository = userRepository;
        _jwtTokenGenerator = jwtTokenGenerator;
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
        return new AuthResponse
        {
            Token = _jwtTokenGenerator.GenerateToken(user.Id, user.Email,user.FirstName,user.Role),
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
        return new AuthResponse
        {
            Token = _jwtTokenGenerator.GenerateToken(user.Id, user.Email,user.FirstName,user.Role),
            Email = user.Email,
            FullName = $"{user.FirstName} {user.LastName}"
        };
      //  return _jwtTokenGenerator.GenerateToken(user.Id, user.Email);
    }
}