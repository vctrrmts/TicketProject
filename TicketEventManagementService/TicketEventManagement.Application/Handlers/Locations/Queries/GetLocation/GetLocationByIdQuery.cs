using MediatR;
using TicketEventManagement.Application.DTOs;

namespace TicketEventManagement.Application.Handlers.Locations.Queries.GetLocation;

public class GetLocationByIdQuery : IRequest<GetLocationDto>
{
    public Guid LocationId { get; set; }
}
