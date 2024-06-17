using MediatR;
using Serilog;
using System.Text.Json;
using UsersManagement.Application.Abstractions.ExternalRepositories;
using UsersManagement.Application.Abstractions.Persistence;
using UsersManagement.Application.Exceptions;
using UsersManagement.Domain;

namespace UsersManagement.Application.Handlers.Command.DeleteUser;

public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand>
{
    private readonly IBaseRepository<User> _users;

    private readonly IUsersGRPCRepository _usersGRPCRepository;

    public DeleteUserCommandHandler(IBaseRepository<User> users, IUsersGRPCRepository usersGRPCRepository)
    {
        _users = users;
        _usersGRPCRepository = usersGRPCRepository;
    }
    public async Task Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _users.SingleOrDefaultAsync(x => x.UserId == request.UserId, cancellationToken);
        if (user == null)
        {
            throw new NotFoundException($"User with Id = {request.UserId} not found");
        }

        await _users.DeleteAsync(user, cancellationToken);
        await _usersGRPCRepository.DeleteUserAsync(user.UserId.ToString(), cancellationToken);
        Log.Information("User deleted " + JsonSerializer.Serialize(request));
    }
}
