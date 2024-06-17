using Authorization.Domain;
using MediatR;
using Serilog;
using System.Text.Json;
using Authorization.Application.Abstractions.Persistence;
using Authorization.Application.Abstractions.Service;
using Authorization.Application.Utils;

namespace Authorization.Application.Handlers.Command.UpdatePassword;

public class UpdatePasswordCommandHandler : IRequestHandler<UpdatePasswordCommand>
{
    private readonly IBaseRepository<User> _users;

    public UpdatePasswordCommandHandler(IBaseRepository<User> users)
    {
        _users = users;
    }

    public async Task Handle(UpdatePasswordCommand request, CancellationToken cancellationToken)
    {
        User user = await _users.SingleAsync(x => x.UserId.ToString() == request.UserId, cancellationToken);

        user.UpdatePasswordHash(request.PasswordHash);

        await _users.UpdateAsync(user, cancellationToken);
        Log.Information("User's password updated " + user.Login);
    }
}
