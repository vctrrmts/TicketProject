using MediatR;
using TicketEventManagement.Application.DTOs;

namespace TicketEventManagement.Application.Handlers.Categories.Commands.CreateCategory;

public class CreateCategoryCommand : IRequest<GetCategoryDto>
{
    public string Name { get; set; } = default!;
}
