using MediatR;
using TicketEventManagement.Application.DTOs;

namespace TicketEventManagement.Application.Handlers.Cities.Queries.GetListCity;

public class GetCityListQuery : IRequest<IReadOnlyCollection<GetCityDto>>
{
    public int? Offset { get; set; }
    public int? Limit { get; set; }
    public string? FreeText { get; set; }
    public bool? IsActive { get; set; }
}
