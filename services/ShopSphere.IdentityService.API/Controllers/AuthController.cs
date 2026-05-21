using Microsoft.AspNetCore.Mvc;
using ShopSphere.IdentityService.Application.DTOs;
using ShopSphere.IdentityService.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
namespace ShopSphere.IdentityService.API.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(
        RegisterRequestDto request)
    {
        var response =
            await _authService.RegisterAsync(request);

        return Ok(response);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(
        LoginRequestDto request)
    {
        var response =
            await _authService.LoginAsync(request);

        return Ok(response);
    }
   [Authorize]
    [HttpGet("me")]
    public IActionResult GetCurrentUser()
    {
        var email =
            User.FindFirst(ClaimTypes.Email)?.Value;

        var firstName =
            User.FindFirst(ClaimTypes.GivenName)?.Value;

        return Ok(new
        {
            Email = email,
            FirstName = firstName
        });
    }
    [Authorize(Roles = "Admin")]
    [HttpGet("admin-only")]
    public IActionResult AdminOnly()
    {
        return Ok("Welcome Admin");
    }
    [Authorize(Roles = "User")]
    [HttpGet("user-only")]
    public IActionResult UserOnly()
    {
        return Ok("Welcome User");
    }

    [HttpPost("refresh")]
    public async Task<IActionResult> Refresh(
    RefreshTokenRequestDto request)
    {
        var response =
            await _authService.RefreshTokenAsync(request);

        return Ok(response);
    }

    [Authorize]
    [HttpGet("profile")]
    public async Task<IActionResult> GetProfile()
    {
        var userId =
            User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (string.IsNullOrEmpty(userId))
        {
            return Unauthorized();
        }

        var profile =
            await _authService.GetProfileAsync(
                Guid.Parse(userId));

        return Ok(profile);
    }
    [Authorize]
    [HttpPut("profile")]
    public async Task<IActionResult> UpdateProfile(UpdateProfileDto request)
    {
        var userId =
            User.FindFirstValue(
                ClaimTypes.NameIdentifier);

        if (string.IsNullOrEmpty(userId))
        {
            return Unauthorized();
        }

        await _authService.UpdateProfileAsync(
            Guid.Parse(userId),
            request);

        return Ok(new
        {
            Message = "Profile updated successfully"
        });
    }
}