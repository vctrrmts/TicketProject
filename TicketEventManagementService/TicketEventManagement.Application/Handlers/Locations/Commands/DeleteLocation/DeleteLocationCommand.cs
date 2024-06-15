using MediatR;

namespace TicketEventManagement.Application.Handlers.Locations.Commands.DeleteLocation;

public class DeleteLocationCommand : IRequest
{
    public Guid LocationId { get; set; }
}
