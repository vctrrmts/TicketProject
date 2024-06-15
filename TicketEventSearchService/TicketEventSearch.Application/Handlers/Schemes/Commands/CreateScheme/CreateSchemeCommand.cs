using MediatR;

namespace TicketEventSearch.Application.Handlers.Schemes.Commands.CreateScheme;

public class CreateSchemeCommand : IRequest
{
    public Guid SchemeId { get; set; }
    public Guid LocationId { get; set; }
    public string Name { get; set; } = default!;
}
