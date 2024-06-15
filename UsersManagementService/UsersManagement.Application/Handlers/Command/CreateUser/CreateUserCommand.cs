using MediatR;
using UsersManagement.Application.DTOs;

namespace UsersManagement.Application.Handlers.Command.CreateUser;

public class CreateUserCommand : IRequest<GetUserDto>
{
    public string Login { get; set; } = default!;
    public string Password { get; set; } = default!;
}
