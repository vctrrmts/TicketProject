using MediatR;
using TicketEventManagement.Application.DTOs;

namespace TicketEventManagement.Application.Handlers.Schemes.Commands.CreateScheme;

public class CreateSchemeCommand : IRequest<GetSchemeDto>
{
    public Guid LocationId { get; set; }
    public string Name { get; set; } = default!;
}
