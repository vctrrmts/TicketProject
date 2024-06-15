using MediatR;

namespace TicketEventSearch.Application.Handlers.Schemes.Commands.UpdateScheme;

public class UpdateSchemeCommand : IRequest
{
    public Guid SchemeId { get; set; }
    public string Name { get; set; } = default!;
    public bool IsActive { get; set; }
}
