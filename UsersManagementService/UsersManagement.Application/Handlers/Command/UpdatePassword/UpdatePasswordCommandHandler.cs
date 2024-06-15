using MediatR;
using Serilog;
using System.Text.Json;
using UsersManagement.Application.Abstractions.Persistence;
using UsersManagement.Application.Abstractions.Service;
using UsersManagement.Application.Utils;
using UsersManagement.Domain;

namespace UsersManagement.Application.Handlers.Command.UpdatePassword;

public class UpdatePasswordCommandHandler : IRequestHandler<UpdatePasswordCommand>
{
    private readonly ICurrentUserService _currentUserService;
    private readonly IBaseRepository<User> _users;

    public UpdatePasswordCommandHandler(
        ICurrentUserService currentUserService,
        IBaseRepository<User> users)
    {
        _currentUserService = currentUserService;
        _users = users;
    }

    public async Task Handle(UpdatePasswordCommand request, CancellationToken cancellationToken)
    {
        User user = await _users.SingleAsync(x => x.UserId == _currentUserService.CurrentUserId, cancellationToken);

        user.UpdatePasswordHash(PasswordHashUtil.HashPassword(request.Password));

        await _users.UpdateAsync(user, cancellationToken);
        Log.Information("User's password updated " + user.Login);
    }
}
