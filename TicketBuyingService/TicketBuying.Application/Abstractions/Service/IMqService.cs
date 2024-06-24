using Microsoft.Extensions.Configuration;

namespace TicketBuying.Application.Abstractions.Service;

public interface IMqService
{
    void SendMessageToExchange(string exchange, string message, IConfiguration configuration);
}
