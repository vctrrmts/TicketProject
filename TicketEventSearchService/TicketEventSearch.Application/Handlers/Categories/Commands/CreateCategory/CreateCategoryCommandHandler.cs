using AutoMapper;
using MediatR;
using Serilog;
using System.Text.Json;
using TicketEventSearch.Application.Abstractions.Persistence;
using TicketEventSearch.Application.Caches.Category;
using TicketEventSearch.Application.Caches.Event;
using TicketEventSearch.Application.DTOs;
using TicketEventSearch.Domain;

namespace TicketEventSearch.Application.Handlers.Categories.Commands.CreateCategory;

internal class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand, GetCategoryDto>
{
    private readonly IBaseRepository<Category> _categories;

    private readonly IMapper _mapper;

    private readonly ICleanCategoryCacheService _cleanCategoryCacheService;

    public CreateCategoryCommandHandler(IBaseRepository<Category> categories, IMapper mapper,
        ICleanCategoryCacheService cleanCategoryCacheService)
    {
        _categories = categories;
        _mapper = mapper;
        _cleanCategoryCacheService = cleanCategoryCacheService;
    }

    public async Task<GetCategoryDto> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
    {
        var newCategory = new Category(request.CategoryId, request.Name, request.IsActive);
        newCategory = await _categories.AddAsync(newCategory, cancellationToken);
        Log.Information("Category added " + JsonSerializer.Serialize(request));
        _cleanCategoryCacheService.ClearListCategoryCaches();
        return _mapper.Map<GetCategoryDto>(newCategory);
    }
}
