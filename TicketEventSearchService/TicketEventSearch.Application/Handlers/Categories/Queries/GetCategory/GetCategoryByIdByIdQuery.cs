using MediatR;
using TicketEventSearch.Application.DTOs;

namespace TicketEventSearch.Application.Handlers.Categories.Queries.GetCategory;

public class GetCategoryByIdQuery : IRequest<GetCategoryDto>
{
    public Guid CategoryById { get; set; }
}
