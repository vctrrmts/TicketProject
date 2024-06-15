using MediatR;
using TicketEventManagement.Application.DTOs;

namespace TicketEventManagement.Application.Handlers.Categories.Queries.GetCategory;

public class GetCategoryByIdQuery : IRequest<GetCategoryDto>
{
    public Guid CategoryById { get; set; }
}
