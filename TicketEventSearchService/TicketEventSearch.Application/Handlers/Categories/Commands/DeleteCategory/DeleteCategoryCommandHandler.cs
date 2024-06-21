using MediatR;
using Serilog;
using System.Text.Json;
using TicketEventSearch.Application.Abstractions.Persistence;
using TicketEventSearch.Application.Caches.Category;
using TicketEventSearch.Application.Exceptions;
using TicketEventSearch.Domain;

namespace TicketEventSearch.Application.Handlers.Categories.Commands.DeleteCategory;

public class DeleteCategoryCommandHandler : IRequestHandler<DeleteCategoryCommand>
{
    private readonly IBaseRepository<Category> _categories;

    private readonly ICleanCategoryCacheService _cleanCategoryCacheService;

    public DeleteCategoryCommandHandler(IBaseRepository<Category> categories, 
        ICleanCategoryCacheService cleanCategoryCacheService)
    {
        _categories = categories;
        _cleanCategoryCacheService = cleanCategoryCacheService;
    }
    public async Task Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
    {
        Category? categoryById = await _categories.SingleOrDefaultAsync(x => x.CategoryId == request.CategoryId,
            cancellationToken);
        if (categoryById == null)
        {
            throw new NotFoundException($"Category with CategoryId = {request.CategoryId} not found");
        }

        categoryById.UpdateIsActive(false);

        await _categories.UpdateAsync(categoryById, cancellationToken);
        Log.Information("Category deleted " + JsonSerializer.Serialize(request));
        _cleanCategoryCacheService.ClearAllCategoryCaches();
    }
}
