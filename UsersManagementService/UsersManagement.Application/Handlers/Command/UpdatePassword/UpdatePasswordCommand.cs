using MediatR;

namespace UsersManagement.Application.Handlers.Command.UpdatePassword;

public class UpdatePasswordCommand : IRequest
{
    public string Password { get; set; } = default!;
}
