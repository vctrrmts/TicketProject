using MediatR;

namespace TicketEventSearch.Application.Handlers.Categories.Commands.DeleteCategory;

public class DeleteCategoryCommand : IRequest
{
    public Guid CategoryId { get; set; }
}
