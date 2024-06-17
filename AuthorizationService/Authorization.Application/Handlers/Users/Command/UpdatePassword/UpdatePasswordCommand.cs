using MediatR;

namespace Authorization.Application.Handlers.Command.UpdatePassword;

public class UpdatePasswordCommand : IRequest
{
    public string UserId { get; set; } = default!;
    public string PasswordHash { get; set; } = default!;
}
