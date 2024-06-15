using AutoMapper;
using Microsoft.Extensions.Logging;
using Serilog;
using System.Text.Json;
using TicketEventSearch.Application.Abstractions.Caches.Category;
using TicketEventSearch.Application.Abstractions.Persistence;
using TicketEventSearch.Application.BaseRealizations;
using TicketEventSearch.Application.DTOs;
using TicketEventSearch.Application.Handlers.Categories.Queries.GetListCategories;
using TicketEventSearch.Domain;

namespace TicketEventSearch.Application.Handlers.Cities.Queries.GetListCity;

public class GetListCategoriesQueryHandler : BaseCashedQuery<GetListCategoriesQuery, IReadOnlyCollection<GetCategoryDto>>
{
    private readonly IBaseRepository<Category> _categories;
    private readonly IMapper _mapper;

    public GetListCategoriesQueryHandler(IBaseRepository<Category> categories, 
        IMapper mapper, ICategoryListCache cache) : base(cache)
    {
        _categories = categories;
        _mapper = mapper;
    }

    public override async Task<IReadOnlyCollection<GetCategoryDto>> SentQueryAsync(GetListCategoriesQuery request, CancellationToken cancellationToken)
    {
        var categories = await _categories.GetListAsync(
            request.Offset,
            request.Limit,
            e => (string.IsNullOrWhiteSpace(request.FreeText) || e.Name.Contains(request.FreeText))
            && e.IsActive == true,
            x => x.Name,
            false,
            cancellationToken);
        return _mapper.Map<IReadOnlyCollection<GetCategoryDto>>(categories);
    }
}
