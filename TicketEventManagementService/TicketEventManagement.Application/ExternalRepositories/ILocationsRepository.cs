using TicketEventManagement.Domain;

namespace TicketEventManagement.Application.ExternalRepositories;

public interface ILocationsRepository
{
    Task CreateLocationAsync(Location location, string accessToken, CancellationToken cancellationToken);
    Task UpdateLocationAsync(Location location, string accessToken, CancellationToken cancellationToken);
    Task DeleteLocationAsync(Guid locationId, string accessToken, CancellationToken cancellationToken);
}
