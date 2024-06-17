using MediatR;

namespace Authorization.Application.Handlers.Command.DeleteUser;

public class DeleteUserCommand : IRequest
{
    public string UserId { get; set; } = default!;  
}
