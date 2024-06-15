using MediatR;
using TicketEventManagement.Application.DTOs;

namespace TicketEventManagement.Application.Handlers.Categories.Commands.UpdateCategory;

public class UpdateCategoryCommand : IRequest<GetCategoryDto>
{
    public Guid CategoryId { get; set; }
    public string Name { get; set; } = default!;
    public bool IsActive { get; set; }
}
