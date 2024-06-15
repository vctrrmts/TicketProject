using MediatR;

namespace TicketEventSearch.Application.Handlers.Locations.Commands.UpdateLocation;

public class UpdateLocationCommand : IRequest
{
    public Guid LocationId { get; set; }
    public string Name { get; set; } = default!;
    public string Address { get; set; } = default!;
    public double Latitude { get; set; }
    public double Longitude { get; set; }
    public bool IsActive { get; set; }
}
