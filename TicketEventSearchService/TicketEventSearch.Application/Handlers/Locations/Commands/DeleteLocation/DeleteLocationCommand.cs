using MediatR;

namespace TicketEventSearch.Application.Handlers.Locations.Commands.DeleteLocation;

public class DeleteLocationCommand : IRequest
{
    public Guid LocationId { get; set; }
}
