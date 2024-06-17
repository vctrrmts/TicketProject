using FluentValidation;

namespace Authorization.Application.Handlers.Command.UpdatePassword;

public class UpdatePasswordCommandValidator : AbstractValidator<UpdatePasswordCommand>
{
    public UpdatePasswordCommandValidator()
    {
        RuleFor(p => p.UserId).NotEmpty();
        RuleFor(p => p.PasswordHash).NotEmpty();
    }
}
