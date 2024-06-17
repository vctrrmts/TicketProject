using Authorization.Application.Handlers.Users.Command.CreateUser;
using Authorization.Domain;
using AutoMapper;
using MediatR;
using Serilog;
using System.Text.Json;
using Authorization.Application.Abstractions.Persistence;
using Authorization.Application.Exceptions;
using Authorization.Application.Utils;

namespace Authorization.Application.Handlers.Command.CreateUser;

public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand>
{
    private readonly IBaseRepository<User> _users;

    public CreateUserCommandHandler(
        IBaseRepository<User> users)
    {
        _users = users;
    }

    public async Task Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        if (await _users.SingleOrDefaultAsync(x => x.Login == request.Login.Trim()) is not null)
        {
            throw new BadOperationException("User login exist");
        }

        var newUser = new User(request.UserId, request.Login, request.PasswordHash);

        await _users.AddAsync(newUser, cancellationToken);
        Log.Information("User added " + JsonSerializer.Serialize(request));
    }
}
