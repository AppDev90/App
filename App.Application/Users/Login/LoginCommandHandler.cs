
using App.Application.Abstractions.Authentication;
using App.Application.Abstractions.Messaging;
using App.Application.Utility;
using App.Domain.Abstraction;
using App.Domain.Shared;
using App.Domain.Users;

namespace App.Application.Users.Login;

public class LoginCommandHandler : ICommandHandler<LoginCommand, string>
{
    private readonly IUserRepository _userRepository;

    private readonly IJwtProvider _jwtProvider;

    public LoginCommandHandler(
        IUserRepository userRepository,
        IJwtProvider jwtProvider)
    {
        _userRepository = userRepository;
        _jwtProvider = jwtProvider;
    }

    public async Task<Result<string>> Handle(
        LoginCommand request,
        CancellationToken cancellationToken)
    {

        var emailResult = Email.Create(request.Email);

        if (emailResult.IsFailure)
        {
            return Result.Failure<string>(emailResult.Error);
        }

        var passwordHash = SHA1.Encode(request.Password);

        var user = await _userRepository.GetByCredentialsAsync(emailResult.Value, passwordHash, cancellationToken);

        if (user is null)
        {
            return Result.Failure<string>(UserErrors.InvalidCredentials);
        }

        var token = _jwtProvider.Generate(user);

        return token;
    }
}
