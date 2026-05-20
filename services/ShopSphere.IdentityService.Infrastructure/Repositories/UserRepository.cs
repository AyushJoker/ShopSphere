using Microsoft.EntityFrameworkCore;
using ShopSphere.IdentityService.Application.Interfaces;
using ShopSphere.IdentityService.Domain.Entities;
using ShopSphere.IdentityService.Infrastructure.Data;

namespace ShopSphere.IdentityService.Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
    private readonly IdentityDbContext _context;

    public UserRepository(IdentityDbContext context)
    {
        _context = context;
    }

    public async Task<User?> GetUserByEmailAsync(string email)
    {
        return await _context.Users
            .FirstOrDefaultAsync(x => x.Email == email);
    }

    public async Task AddUserAsync(User user)
    {
        await _context.Users.AddAsync(user);
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}