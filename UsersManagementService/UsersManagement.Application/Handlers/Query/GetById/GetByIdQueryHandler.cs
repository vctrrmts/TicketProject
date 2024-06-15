using AutoMapper;
using MediatR;
using UsersManagement.Application.Abstractions.Persistence;
using UsersManagement.Domain;
using UsersManagement.Application.DTOs;
using UsersManagement.Application.Exceptions;

namespace UsersManagement.Application.Handlers.Query.GetById;

public class GetByIdQueryHandler : IRequestHandler<GetByIdQuery, GetUserDto>
{
    private readonly IBaseRepository<User> _users;

    private readonly IMapper _mapper;

    public GetByIdQueryHandler(IBaseRepository<User> users, IMapper mapper)
    {
        _users = users;
        _mapper = mapper;
    }

    public async Task<GetUserDto> Handle(GetByIdQuery request, CancellationToken cancellationToken)
    {

        User? user = await _users.SingleOrDefaultAsync(x => x.UserId == request.UserId, cancellationToken);
        if (user == null)
        {
            throw new NotFoundException($"User with Id = {request.UserId} not found");
        }

        return _mapper.Map<User, GetUserDto>(user); ;
    }
}
