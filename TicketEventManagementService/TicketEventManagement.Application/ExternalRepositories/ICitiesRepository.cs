using TicketEventManagement.Domain;

namespace TicketEventManagement.Application.ExternalRepositories;

public interface ICitiesRepository
{
    Task CreateCityAsync(City city, string accessToken, CancellationToken cancellationToken);
    Task UpdateCityAsync(City city, string accessToken, CancellationToken cancellationToken);
    Task DeleteCityAsync(Guid cityId, string accessToken, CancellationToken cancellationToken);
}
