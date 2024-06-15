using TicketEventManagement.Application.DTOs;
using TicketEventManagement.Domain;

namespace TicketEventManagement.Application.ExternalRepositories;

public interface IEventsRepository
{
    Task PublishEventAsync(EventForExportDto myEvent, string accessToken, CancellationToken cancellationToken);
    Task UpdateEventAsync(EventForExportDto myEvent, string accessToken, CancellationToken cancellationToken);
}
