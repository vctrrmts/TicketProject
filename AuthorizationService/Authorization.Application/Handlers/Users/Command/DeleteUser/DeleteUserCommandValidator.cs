using FluentValidation;

namespace Authorization.Application.Handlers.Command.DeleteUser;

public class DeleteUserCommandValidator : AbstractValidator<DeleteUserCommand>
{
    public DeleteUserCommandValidator()
    {
        RuleFor(u => u.UserId).NotEmpty();
    }
}
