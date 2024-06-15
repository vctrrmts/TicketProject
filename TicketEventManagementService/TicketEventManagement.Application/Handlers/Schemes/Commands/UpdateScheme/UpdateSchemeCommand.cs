using MediatR;
using TicketEventManagement.Application.DTOs;

namespace TicketEventManagement.Application.Handlers.Schemes.Commands.UpdateScheme;

public class UpdateSchemeCommand : IRequest<GetSchemeDto>
{
    public Guid SchemeId { get; set; }
    public string Name { get; set; } = default!;
    public bool IsActive { get; set; }
}
