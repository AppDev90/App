

using App.Domain.Abstraction;
using App.Domain.Roles;
using App.Domain.Shared;
using App.Domain.UserRoles;
using App.Domain.Users.Event;

namespace App.Domain.Users;

public class User : Entity<UserId>
{

    private User()
    {

    }

    private User(
        UserId userId,
        Email email,
        string passwordHash)
      : base(userId)
    {
        Email = email;
        PasswordHash = passwordHash;
    }


    public Email Email { get; private set; }

    public string PasswordHash { get; private set; }


    public ICollection<Role> Roles { get; private set; }


    public static Result<User> Create(
        Email email,
        string passwordHash,
        bool isEmailUnique)
    {

        if (!isEmailUnique)
        {
            return Result.Failure<User>(UserErrors.EmailIsNotAvailable);
        }

        var user = new User(
            UserId.New(),
            email,
            passwordHash);

        user.RaiseDomainEvent(new UserCreatedDomainEvent(user.Id));

        return user;
    }

}
