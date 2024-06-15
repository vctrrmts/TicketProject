using MediatR;
using TicketEventManagement.Application.DTOs;

namespace TicketEventManagement.Application.Handlers.Categories.Queries.GetListCategories;

public class GetListCategoriesQuery : IRequest<IReadOnlyCollection<GetCategoryDto>>
{
    public int? Offset { get; set; }
    public int? Limit { get; set; }
    public string? FreeText { get; set; }
    public bool? IsActive { get; set; }
}
