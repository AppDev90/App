

using App.Domain.Roles;
using App.Domain.Users;
using Microsoft.EntityFrameworkCore;

namespace App.Infrastructure.Authentication;

public class PermissionService : IPermissionService
{
    private readonly ApplicationDbContext _context;

    public PermissionService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<HashSet<string>> GetPermissionAsync(UserId userId)
    {
        ICollection<Role>[] roles = await _context.Set<User>()
            .Include(p => p.Roles)
            .ThenInclude(p => p.Permissions)
            .Where(p => p.Id == userId)
            .Select(p => p.Roles)
            .ToArrayAsync();

        return roles
            .SelectMany(p => p)
            .SelectMany(p => p.Permissions)
            .Select(p => p.Name.Value)
            .ToHashSet();

    }
}
