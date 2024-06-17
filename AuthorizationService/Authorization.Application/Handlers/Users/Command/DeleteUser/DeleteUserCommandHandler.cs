using Authorization.Domain;
using MediatR;
using Serilog;
using System.Text.Json;
using Authorization.Application.Abstractions.Persistence;
using Authorization.Application.Exceptions;

namespace Authorization.Application.Handlers.Command.DeleteUser;

public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand>
{
    private readonly IBaseRepository<User> _users;

    public DeleteUserCommandHandler(IBaseRepository<User> users)
    {
        _users = users;
    }
    public async Task Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _users.SingleOrDefaultAsync(x => x.UserId.ToString() == request.UserId, cancellationToken);
        if (user == null)
        {
            throw new NotFoundException($"User with Id = {request.UserId} not found");
        }

        await _users.DeleteAsync(user, cancellationToken);
        Log.Information("User deleted " + JsonSerializer.Serialize(request));
    }
}
