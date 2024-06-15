using Authorization.Domain;

namespace Authorization.Application.Abstractions.ExternalProviders;

public interface IUsersProvider
{
    Task<User> GetUserByLoginAsync(string login, CancellationToken cancellationToken);
}
