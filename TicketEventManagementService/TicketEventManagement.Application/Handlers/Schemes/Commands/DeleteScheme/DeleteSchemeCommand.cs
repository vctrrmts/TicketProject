using MediatR;

namespace TicketEventManagement.Application.Handlers.Schemes.Commands.DeleteScheme;

public class DeleteSchemeCommand : IRequest
{
    public Guid SchemeId { get; set; }
}
