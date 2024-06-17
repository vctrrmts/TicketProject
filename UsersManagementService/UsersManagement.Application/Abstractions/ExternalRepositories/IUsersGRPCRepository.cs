namespace UsersManagement.Application.Abstractions.ExternalRepositories;

public interface IUsersGRPCRepository
{
    Task CreateUserAsync(string userId, string login, string passwordHash, CancellationToken cancellationToken);
    Task UpdatePasswordAsync(string userId, string passwordHash, CancellationToken cancellationToken);
    Task DeleteUserAsync(string userId, CancellationToken cancellationToken);
}
