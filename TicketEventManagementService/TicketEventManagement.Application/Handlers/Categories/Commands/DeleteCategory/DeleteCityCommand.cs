using MediatR;

namespace TicketEventManagement.Application.Handlers.Categories.Commands.DeleteCategory;

public class DeleteCategoryCommand : IRequest
{
    public Guid CategoryId { get; set; }
}
