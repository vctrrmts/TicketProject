using MediatR;

namespace TicketEventSearch.Application.Handlers.Cities.Commands.DeleteCity;

public class DeleteCityCommand :IRequest
{
    public Guid CityId { get; set; }
}
