using MediatR;
using TicketEventSearch.Application.DTOs;

namespace TicketEventSearch.Application.Handlers.Cities.Commands.CreateCity;

public class CreateCityCommand : IRequest<GetCityDto>
{
    public Guid CityId { get; set; }

    public string Name { get; set; }

    public bool IsActive { get; set; }
}
