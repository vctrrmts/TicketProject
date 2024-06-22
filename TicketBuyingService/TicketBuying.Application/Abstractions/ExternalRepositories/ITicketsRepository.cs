using TicketBuying.Domain;

namespace TicketBuying.Application.Abstractions.ExternalProviders;

public interface ITicketsRepository
{
    Task<Ticket[]> UpdateTicketsStatusAsync(Guid[] ticketId, CancellationToken cancellationToken);
}
