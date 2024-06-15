using AutoMapper;
using TicketEventSearch.Application.Abstractions.Caches.Category;
using TicketEventSearch.Application.Abstractions.Persistence;
using TicketEventSearch.Application.BaseRealizations;
using TicketEventSearch.Application.DTOs;
using TicketEventSearch.Application.Exceptions;
using TicketEventSearch.Domain;

namespace TicketEventSearch.Application.Handlers.Categories.Queries.GetCategory;

public class GetCategoryByIdQueryHandler : BaseCashedQuery<GetCategoryByIdQuery, GetCategoryDto>
{
    private readonly IBaseRepository<Category> _categories;

    private readonly IMapper _mapper;

    public GetCategoryByIdQueryHandler(IBaseRepository<Category> categories
        , IMapper mapper, ICategoryCache cache) : base(cache)
    {
        _categories = categories;
        _mapper = mapper;
    }

    public override async Task<GetCategoryDto> SentQueryAsync(GetCategoryByIdQuery request, CancellationToken cancellationToken)
    {
        Category? category = await _categories.SingleOrDefaultAsync(x => x.CategoryId == request.CategoryById,
            cancellationToken);

        if (category == null)
        {
            throw new NotFoundException($"Category with id = {request.CategoryById} not found");
        }

        return _mapper.Map<GetCategoryDto>(category);
    }
}
