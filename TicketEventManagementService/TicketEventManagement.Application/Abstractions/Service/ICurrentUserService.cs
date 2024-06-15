namespace TicketEventManagement.Application.Abstractions.Service;

public interface ICurrentUserService
{
    public int CurrentUserId { get; }
    public string[] CurrentUserRoles { get; }

    public string AccessToken { get; }
}
