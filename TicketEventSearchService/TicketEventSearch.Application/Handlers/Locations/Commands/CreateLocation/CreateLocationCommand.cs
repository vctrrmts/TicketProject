using MediatR;

namespace TicketEventSearch.Application.Handlers.Locations.Commands.CreateLocation;

public class CreateLocationCommand : IRequest
{
    public Guid LocationId { get; set; }
    public Guid CityId { get; set; }
    public string Name { get; set; } = default!;
    public string Address { get; set; } = default!;
    public double Latitude { get; set; }
    public double Longitude { get; set; }
    public bool IsActive { get; set; }
}
