using FluentValidation;

namespace UsersManagement.Application.Handlers.Command.UpdatePassword;

public class UpdatePasswordCommandValidator : AbstractValidator<UpdatePasswordCommand>
{
    public UpdatePasswordCommandValidator()
    {
        RuleFor(p => p.Password).Length(8, 50).NotEmpty();
    }
}
