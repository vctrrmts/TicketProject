using AutoMapper;
using MediatR;
using Serilog;
using System.Text.Json;
using UsersManagement.Application.Abstractions.ExternalRepositories;
using UsersManagement.Application.Abstractions.Persistence;
using UsersManagement.Application.DTOs;
using UsersManagement.Application.Exceptions;
using UsersManagement.Application.Utils;
using UsersManagement.Domain;

namespace UsersManagement.Application.Handlers.Command.CreateUser;

public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, GetUserDto>
{
    private readonly IBaseRepository<User> _users;
    private readonly IMapper _mapper;
    
    private readonly IUsersGRPCRepository _usersGRPCRepository;

    public CreateUserCommandHandler(
        IBaseRepository<User> users,
        IMapper mapper,
        IUsersGRPCRepository usersGRPCRepository)
    {
        _users = users;
        _mapper = mapper;
        _usersGRPCRepository = usersGRPCRepository;
    }

    public async Task<GetUserDto> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        if (await _users.SingleOrDefaultAsync(x => x.Login == request.Login.Trim()) is not null)
        {
            throw new BadOperationException("User login exist");
        }

        var newUser = new User(request.Login.Trim(), PasswordHashUtil.HashPassword(request.Password));

        await _users.AddAsync(newUser, cancellationToken);
        await _usersGRPCRepository.CreateUserAsync(newUser.UserId.ToString(), newUser.Login, 
            newUser.PasswordHash, cancellationToken);

        Log.Information("User added " + JsonSerializer.Serialize(request));

        var getUserDto = _mapper.Map<User, GetUserDto>(newUser);
        return getUserDto;
    }
}
