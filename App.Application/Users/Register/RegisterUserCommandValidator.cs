using FluentValidation;

namespace App.Application.Users.Register;

internal sealed class RegisterUserCommandValidator : AbstractValidator<RegisterUserCommand>
{
    public RegisterUserCommandValidator()
    {
        RuleFor(c => c.Email).EmailAddress();

        RuleFor(c => c.Password).NotEmpty().MinimumLength(5);
    }
}
