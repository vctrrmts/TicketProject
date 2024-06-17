using Authorization.Application.Handlers.Users.Command.CreateUser;
using FluentValidation;

namespace Authorization.Application.Handlers.Command.CreateUser;

public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
{
    public CreateUserCommandValidator()
    {
        RuleFor(x=>x.UserId).NotEmpty();
        RuleFor(x => x.Login).Length(3, 50).NotEmpty();
        RuleFor(x => x.PasswordHash).NotEmpty();
    }
}
