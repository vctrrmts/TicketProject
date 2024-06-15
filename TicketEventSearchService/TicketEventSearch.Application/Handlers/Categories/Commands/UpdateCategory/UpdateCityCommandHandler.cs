using AutoMapper;
using MediatR;
using Serilog;
using System.Text.Json;
using TicketEventSearch.Application.Abstractions.Persistence;
using TicketEventSearch.Application.Caches.Category;
using TicketEventSearch.Application.DTOs;
using TicketEventSearch.Application.Exceptions;
using TicketEventSearch.Domain;

namespace TicketEventSearch.Application.Handlers.Categories.Commands.UpdateCategory;

internal class UpdateCategoryCommandHandler : IRequestHandler<UpdateCategoryCommand, GetCategoryDto>
{
    private readonly IBaseRepository<Category> _categories;

    private readonly IMapper _mapper;

    private readonly ICleanCategoryCacheService _cleanCategoryCacheService;

    public UpdateCategoryCommandHandler(IBaseRepository<Category> categories, IMapper mapper,
        ICleanCategoryCacheService cleanCategoryCacheService)
    {
        _categories = categories;
        _mapper = mapper;
        _cleanCategoryCacheService = cleanCategoryCacheService;
    }
    public async Task<GetCategoryDto> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
    {
        Category? category = await _categories.SingleOrDefaultAsync(x => x.CategoryId == request.CategoryId, cancellationToken);
        if (category == null)
        {
            throw new NotFoundException($"Category with CategoryId = {request.CategoryId} not found");
        }

        category.UpdateName(request.Name);
        category.UpdateIsActive(request.IsActive);
        await _categories.UpdateAsync(category, cancellationToken);
        Log.Information("Category updated " + JsonSerializer.Serialize(request));

        _cleanCategoryCacheService.ClearAllCategoryCaches();

        return _mapper.Map<GetCategoryDto>(category);
    }
}
