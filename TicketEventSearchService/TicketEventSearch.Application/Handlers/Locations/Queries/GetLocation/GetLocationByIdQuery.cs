using MediatR;
using TicketEventSearch.Application.DTOs;

namespace TicketEventSearch.Application.Handlers.Locations.Queries.GetLocation;

public class GetLocationByIdQuery : IRequest<GetLocationDto>
{
    public Guid LocationId { get; set; }
}
