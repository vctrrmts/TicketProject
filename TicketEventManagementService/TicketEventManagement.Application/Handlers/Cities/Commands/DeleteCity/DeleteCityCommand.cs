using MediatR;

namespace TicketEventManagement.Application.Handlers.Cities.Commands.DeleteCity;

public class DeleteCityCommand :IRequest
{
    public Guid CityId { get; set; }
}
