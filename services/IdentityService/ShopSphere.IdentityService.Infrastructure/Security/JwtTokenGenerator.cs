using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using ShopSphere.IdentityService.Application.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ShopSphere.IdentityService.Infrastructure.Security;

public class JwtTokenGenerator : IJwtTokenGenerator
{
    private readonly IConfiguration _configuration;

    public JwtTokenGenerator(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public string GenerateToken(Guid userId, string email,string firstName,string role)
    {
        var claims = new[]
         {
            new Claim(ClaimTypes.NameIdentifier,userId.ToString()),

            new Claim(JwtRegisteredClaimNames.Sub, userId.ToString()),

            new Claim(JwtRegisteredClaimNames.Email, email),

            new Claim(ClaimTypes.Email, email),

            new Claim(ClaimTypes.GivenName, firstName),

            new Claim(ClaimTypes.Role, role)
        };

        var key = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!));

        var creds = new SigningCredentials(
            key,
            SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: _configuration["Jwt:Issuer"],
            audience: _configuration["Jwt:Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddHours(2),
            signingCredentials: creds);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}