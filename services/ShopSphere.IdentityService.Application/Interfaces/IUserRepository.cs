using ShopSphere.IdentityService.Domain.Entities;

namespace ShopSphere.IdentityService.Application.Interfaces;

public interface IUserRepository
{
    Task<User?> GetUserByEmailAsync(string email);

    Task AddUserAsync(User user);

    Task SaveChangesAsync();
}