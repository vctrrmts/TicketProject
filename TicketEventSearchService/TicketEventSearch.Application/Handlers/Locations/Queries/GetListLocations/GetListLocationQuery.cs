using MediatR;
using TicketEventSearch.Application.DTOs;

namespace TicketEventSearch.Application.Handlers.Locations.Queries.GetListLocations;

public class GetListLocationQuery : IRequest<IReadOnlyCollection<GetLocationDto>>
{
    public int? Offset { get; set; }
    public int? Limit { get; set; }
    public string? FreeText { get; set; }
}
