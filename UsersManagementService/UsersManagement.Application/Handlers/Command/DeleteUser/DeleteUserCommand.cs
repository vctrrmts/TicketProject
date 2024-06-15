using MediatR;

namespace UsersManagement.Application.Handlers.Command.DeleteUser;

public class DeleteUserCommand : IRequest
{
    public Guid UserId { get; set; }
}
