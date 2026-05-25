
using System.Security.Cryptography;
using ShopSphere.IdentityService.Application.Interfaces;

namespace ShopSphere.IdentityService.Infrastructure.Security
{
    public class RefreshTokenGenerator:IRefreshTokenGenerator
    {
        public string Generate()
        {
            var randomNumber = new byte[64];

            using var rng = RandomNumberGenerator.Create();

            rng.GetBytes(randomNumber);

            return Convert.ToBase64String(randomNumber);
        }
    }
}
