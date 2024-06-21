using TicketEventManagement.Application.DTOs;
using TicketEventManagement.Domain;

namespace TicketEventManagement.Application.ExternalRepositories;

public interface ISeatsRepository
{
    Task AddRangeOfSeatsAsync(SeatForExportDto[] seats, string accessToken, CancellationToken cancellationToken);
    Task RemoveRangeOfSeatsAsync(Guid[] seatIds, string accessToken, CancellationToken cancellationToken);
}
