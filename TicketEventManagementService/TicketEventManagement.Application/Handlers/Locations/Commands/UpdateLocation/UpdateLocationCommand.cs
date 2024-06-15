using MediatR;
using TicketEventManagement.Application.DTOs;

namespace TicketEventManagement.Application.Handlers.Locations.Commands.UpdateLocation;

public class UpdateLocationCommand : IRequest<GetLocationDto>
{
    public Guid LocationId { get; set; }
    public string Name { get; set; } = default!;
    public string Address { get; set; } = default!;
    public double Latitude { get; set; }
    public double Longitude { get; set; }
    public bool IsActive { get; set; }
}
