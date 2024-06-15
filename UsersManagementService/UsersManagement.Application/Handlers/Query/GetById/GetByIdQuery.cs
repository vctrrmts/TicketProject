using MediatR;
using UsersManagement.Application.DTOs;

namespace UsersManagement.Application.Handlers.Query.GetById;

public class GetByIdQuery : IRequest<GetUserDto>
{
    public Guid UserId { get; set; }
}
