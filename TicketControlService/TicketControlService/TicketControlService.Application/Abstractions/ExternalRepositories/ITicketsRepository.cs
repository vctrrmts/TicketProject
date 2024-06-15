using TicketControlService.Domain;

namespace TicketControlService.Application.Abstractions.ExternalProviders;

public interface ITicketsRepository
{
    Task<Ticket> GetTicketAsync(string hashFromQR, CancellationToken cancellationToken);
}
