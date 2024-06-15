namespace Authorization.Application.Abstractions.Service;

public interface ICurrentUserService
{
    public Guid CurrentUserId { get; }
    public string[] CurrentUserRoles { get; }
}
