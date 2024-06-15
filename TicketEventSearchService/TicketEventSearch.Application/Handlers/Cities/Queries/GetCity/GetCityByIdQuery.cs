using MediatR;
using TicketEventSearch.Application.DTOs;

namespace TicketEventSearch.Application.Handlers.Cities.Queries.GetCity;

public class GetCityByIdQuery : IRequest<GetCityDto>
{
    public Guid CityId { get; set; }
}
