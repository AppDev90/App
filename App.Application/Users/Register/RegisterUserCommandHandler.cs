using App.Application.Abstractions.Messaging;
using App.Application.Utility;
using App.Domain.Abstraction;
using App.Domain.Shared;
using App.Domain.Users;

namespace App.Application.Users.Register;

public sealed class RegisterUserCommandHandler : ICommandHandler<RegisterUserCommand, Guid>
{
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;


    public RegisterUserCommandHandler(IUserRepository userRepository, IUnitOfWork unitOfWork)
    {
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<Guid>> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        var email = Email.Create(request.Email);

        if (email.IsFailure)
        {
            return Result.Failure<Guid>(email.Error);
        }

        var passwordHash = SHA1.Encode(request.Password);

        var isEmailUnique = await _userRepository.
            IsEmailUniqueAsync(email.Value, cancellationToken);

        var user = User.Create(
            email.Value,
            passwordHash,
            isEmailUnique);

        if (user.IsFailure)
        {
            return Result.Failure<Guid>(user.Error);
        }

        _userRepository.Add(user.Value);

        await _unitOfWork.SaveChangesAsync();

        return user.Value.Id.Value;

    }
}
