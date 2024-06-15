using FluentValidation;

namespace UsersManagement.Application.Handlers.Command.CreateUser;

public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
{
    public CreateUserCommandValidator()
    {
        RuleFor(x => x.Login).Length(3, 50).NotEmpty();
        RuleFor(x => x.Password).Length(8, 50).NotEmpty();
    }
}
