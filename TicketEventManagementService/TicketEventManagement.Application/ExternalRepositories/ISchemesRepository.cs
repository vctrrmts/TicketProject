using TicketEventManagement.Domain;

namespace TicketEventManagement.Application.ExternalRepositories;

public interface ISchemesRepository
{
    Task CreateSchemeAsync(Scheme scheme, string accessToken, CancellationToken cancellationToken);
    Task UpdateSchemeAsync(Scheme scheme, string accessToken, CancellationToken cancellationToken);
}
