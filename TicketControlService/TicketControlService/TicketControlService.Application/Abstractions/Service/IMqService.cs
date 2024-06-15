namespace TicketControlService.Application.Abstractions.Service;

public interface IMqService
{
    void SendMessageToExchange(string exchange, string message);
}
