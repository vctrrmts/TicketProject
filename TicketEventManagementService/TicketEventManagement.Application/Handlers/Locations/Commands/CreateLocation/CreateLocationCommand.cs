using MediatR;
using TicketEventManagement.Application.DTOs;

namespace TicketEventManagement.Application.Handlers.Locations.Commands.CreateLocation;

public class CreateLocationCommand : IRequest<GetLocationDto>
{
    public Guid CityId { get; set; }
    public string Name { get; set; } = default!;
    public string Address { get; set; } = default!;
    public double Latitude { get; set; }
    public double Longitude { get; set; }
}
