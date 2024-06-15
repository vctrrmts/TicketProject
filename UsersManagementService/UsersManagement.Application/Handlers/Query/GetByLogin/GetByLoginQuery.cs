using MediatR;
using UsersManagement.Application.DTOs;

namespace UsersManagement.Application.Handlers.Query.GetByLogin;

public class GetByLoginQuery : IRequest<GetUserDto>
{
    public string Login { get; set; } = default!;
}
