using MediatR;
using TicketEventManagement.Application.DTOs;

namespace TicketEventManagement.Application.Handlers.Cities.Queries.GetCity;

public class GetCityByIdQuery : IRequest<GetCityDto>
{
    public Guid CityId { get; set; }
}
