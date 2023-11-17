

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

    public async Task<User> GetByEmailAsync(Email email, CancellationToken cancellationToken = default)
    {
        return await DbContext.Set<User>()
            .Where(p => p.Email == email)
            .FirstOrDefaultAsync(cancellationToken);
    }
}
