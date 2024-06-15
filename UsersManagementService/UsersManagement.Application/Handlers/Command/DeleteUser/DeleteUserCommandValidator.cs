using FluentValidation;

namespace UsersManagement.Application.Handlers.Command.DeleteUser;

public class DeleteUserCommandValidator : AbstractValidator<DeleteUserCommand>
{
    public DeleteUserCommandValidator()
    {
        RuleFor(u => u.UserId).NotEmpty();
    }
}
