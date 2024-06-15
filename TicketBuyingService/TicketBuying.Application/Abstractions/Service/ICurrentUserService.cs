namespace TicketBuying.Application.Abstractions.Service;

public interface ICurrentUserService
{
    public int CurrentUserId { get; }
    public string[] CurrentUserRoles { get; }
}
