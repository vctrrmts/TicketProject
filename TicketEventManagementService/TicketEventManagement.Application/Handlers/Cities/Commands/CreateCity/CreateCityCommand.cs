using MediatR;
using TicketEventManagement.Application.DTOs;

namespace TicketEventManagement.Application.Handlers.Cities.Commands.CreateCity;

public class CreateCityCommand : IRequest<GetCityDto>
{
    public string Name { get; set; } = default!;
}
