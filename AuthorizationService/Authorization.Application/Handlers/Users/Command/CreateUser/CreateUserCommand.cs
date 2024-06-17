using MediatR;

namespace Authorization.Application.Handlers.Users.Command.CreateUser;

public class CreateUserCommand : IRequest
{
    public string UserId { get; set; } = default!;
    public string Login { get; set; } = default!;
    public string PasswordHash { get; set; } = default!;
}
