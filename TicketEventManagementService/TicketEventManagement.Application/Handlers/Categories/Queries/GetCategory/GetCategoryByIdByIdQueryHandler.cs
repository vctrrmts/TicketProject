using AutoMapper;
using MediatR;
using TicketEventManagement.Application.Abstractions.Persistence;
using TicketEventManagement.Application.DTOs;
using TicketEventManagement.Application.Exceptions;
using TicketEventManagement.Domain;

namespace TicketEventManagement.Application.Handlers.Categories.Queries.GetCategory;

public class GetCategoryByIdQueryHandler : IRequestHandler<GetCategoryByIdQuery, GetCategoryDto>
{
    private readonly IBaseRepository<Category> _categories;

    private readonly IMapper _mapper;

    public GetCategoryByIdQueryHandler(IBaseRepository<Category> categories, IMapper mapper)
    {
        _categories = categories;
        _mapper = mapper;
    }

    public async Task<GetCategoryDto> Handle(GetCategoryByIdQuery request, CancellationToken cancellationToken)
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
