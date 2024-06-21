using MediatR;
using TicketEventSearch.Application.DTOs;

namespace TicketEventSearch.Application.Handlers.Cities.Commands.UpdateCity
{
    public class UpdateCityCommand :IRequest<GetCityDto>
    {
        public Guid CityId { get; set; }
        public string Name { get; set; } = default!;
        public bool IsActive { get; set; }
    }
}
