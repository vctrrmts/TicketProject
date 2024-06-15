using AutoMapper;
using UsersManagement.Application.Abstractions.Persistence;
using UsersManagement.Domain;
using UsersManagement.Application.DTOs;
using UsersManagement.Application.Exceptions;
using MediatR;

namespace UsersManagement.Application.Handlers.Query.GetByLogin;

public class GetByLoginQueryHandler : IRequestHandler<GetByLoginQuery, GetUserDto>
{
    private readonly IBaseRepository<User> _users;

    private readonly IMapper _mapper;

    public GetByLoginQueryHandler(IBaseRepository<User> users, IMapper mapper)
    {
        _users = users;
        _mapper = mapper;
    }

    public async Task<GetUserDto> Handle(GetByLoginQuery request, CancellationToken cancellationToken)
    {

        User? user = await _users.SingleOrDefaultAsync(x => x.Login == request.Login, cancellationToken);
        if (user == null)
        {
            throw new NotFoundException($"User with Login = {request.Login} not found");
        }

        return _mapper.Map<User, GetUserDto>(user); ;
    }
}
