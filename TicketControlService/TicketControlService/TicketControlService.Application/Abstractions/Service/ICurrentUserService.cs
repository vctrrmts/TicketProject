namespace TicketControlService.Application.Abstractions.Service;

public interface ICurrentUserService
{
    public int CurrentUserId { get; }
    public string AccessToken { get; }
}
