using MediatR;
using TicketEventSearch.Application.DTOs;

namespace TicketEventSearch.Application.Handlers.Categories.Queries.GetListCategories;

public class GetListCategoriesQuery : IRequest<IReadOnlyCollection<GetCategoryDto>>
{
    public int? Offset { get; set; }
    public int? Limit { get; set; }
    public string? FreeText { get; set; }
}
