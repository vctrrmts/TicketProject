using TicketEventManagement.Domain;

namespace TicketEventManagement.Application.ExternalRepositories;

public interface ICategoryRepository
{
    Task CreateCategoryAsync(Category category, string accessToken, CancellationToken cancellationToken);
    Task UpdateCategoryAsync(Category category, string accessToken, CancellationToken cancellationToken);
    Task DeleteCategoryAsync(Guid categoryId, string accessToken, CancellationToken cancellationToken);
}
