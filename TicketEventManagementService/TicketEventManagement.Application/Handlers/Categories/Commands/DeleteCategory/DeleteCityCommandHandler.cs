using MediatR;
using Serilog;
using System.Text.Json;
using TicketEventManagement.Application.Abstractions.Persistence;
using TicketEventManagement.Application.Abstractions.Service;
using TicketEventManagement.Application.Exceptions;
using TicketEventManagement.Application.ExternalRepositories;
using TicketEventManagement.Domain;

namespace TicketEventManagement.Application.Handlers.Categories.Commands.DeleteCategory;

internal class DeleteCategoryCommandHandler : IRequestHandler<DeleteCategoryCommand>
{
    private readonly IBaseRepository<Category> _categories;

    private readonly ICategoryRepository _categoryRepository;

    private readonly ICurrentUserService _currentUserService;

    public DeleteCategoryCommandHandler(IBaseRepository<Category> categories, 
        ICategoryRepository categoryRepository, ICurrentUserService currentUserService)
    {
        _categories = categories;
        _categoryRepository = categoryRepository;
        _currentUserService = currentUserService;
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

        string token = _currentUserService.AccessToken;
        await _categoryRepository.DeleteCategoryAsync(categoryById.CategoryId, token, cancellationToken);
        Log.Information("Category deleted " + JsonSerializer.Serialize(request));
    }
}
