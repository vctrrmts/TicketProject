using TicketBuying.Domain;

namespace TicketBuying.Application.Abstractions.ExternalProviders;

public interface ITicketsRepository
{
    Task<Ticket> UpdateTicketStatusAsync(Guid ticketId, CancellationToken cancellationToken);
}
