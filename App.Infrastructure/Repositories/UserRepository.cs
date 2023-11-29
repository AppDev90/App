
using App.Domain.Shared;
using App.Domain.Users;
using Microsoft.EntityFrameworkCore;

namespace App.Infrastructure.Repositories;

internal sealed class UserRepository : Repository<User, UserId>, IUserRepository
{
    public UserRepository(ApplicationDbContext dbContext) :
        base(dbContext)
    {
    }

    public async Task<User> GetByCredentialsAsync(Email email, string passwordHash, CancellationToken cancellationToken = default)
    {
        return await DbContext.Set<User>()
            .Where(p => p.Email == email && p.PasswordHash == passwordHash)
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<User> GetByEmailAsync(Email email, CancellationToken cancellationToken = default)
    {
        return await DbContext.Set<User>()
            .Where(p => p.Email == email)
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<bool> IsEmailUniqueAsync(Email email, CancellationToken cancellationToken = default)
    {
        var isEmailExist = await DbContext.Set<User>()
            .AnyAsync(p => p.Email == email, cancellationToken);

        return !isEmailExist;
    }
}
