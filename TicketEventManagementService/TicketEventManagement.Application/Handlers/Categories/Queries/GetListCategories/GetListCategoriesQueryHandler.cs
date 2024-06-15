using AutoMapper;
using MediatR;
using TicketEventManagement.Application.Abstractions.Persistence;
using TicketEventManagement.Application.DTOs;
using TicketEventManagement.Application.Handlers.Categories.Queries.GetListCategories;
using TicketEventManagement.Domain;

namespace TicketEventManagement.Application.Handlers.Cities.Queries.GetListCity;

public class GetListCategoriesQueryHandler : IRequestHandler<GetListCategoriesQuery, IReadOnlyCollection<GetCategoryDto>>
{
    private readonly IBaseRepository<Category> _categories;
    private readonly IMapper _mapper;

    public GetListCategoriesQueryHandler(IBaseRepository<Category> categories, IMapper mapper) 
    {
        _categories = categories;
        _mapper = mapper;
    }
    public async Task<IReadOnlyCollection<GetCategoryDto>> Handle(GetListCategoriesQuery request, CancellationToken cancellationToken)
    {
        var categories = await _categories.GetListAsync(
            request.Offset,
            request.Limit,
            e => (string.IsNullOrWhiteSpace(request.FreeText) || e.Name.Contains(request.FreeText))
            && ((request.IsActive == null) || (e.IsActive == request.IsActive)),
            x => x.Name,
            false,
            cancellationToken);

        return  _mapper.Map<IReadOnlyCollection<GetCategoryDto>>(categories);

    }
}
