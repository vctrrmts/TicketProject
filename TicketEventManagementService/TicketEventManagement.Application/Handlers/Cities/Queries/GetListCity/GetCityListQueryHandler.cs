using AutoMapper;
using MediatR;
using TicketEventManagement.Application.Abstractions.Persistence;
using TicketEventManagement.Application.DTOs;
using TicketEventManagement.Application.Exceptions;
using TicketEventManagement.Domain;

namespace TicketEventManagement.Application.Handlers.Cities.Queries.GetListCity;

public class GetCityListQueryHandler : IRequestHandler<GetCityListQuery, IReadOnlyCollection<GetCityDto>>
{
    private readonly IBaseRepository<City> _cities;
    private readonly IMapper _mapper;

    public GetCityListQueryHandler(IBaseRepository<City> cities, IMapper mapper) 
    {
        _cities = cities;
        _mapper = mapper;
    }
    public async Task<IReadOnlyCollection<GetCityDto>> Handle(GetCityListQuery request, CancellationToken cancellationToken)
    {
        var cities = await _cities.GetListAsync(
            request.Offset,
            request.Limit,
            e => (string.IsNullOrWhiteSpace(request.FreeText) || e.Name.Contains(request.FreeText))
            && ((request.IsActive == null) || (e.IsActive == request.IsActive)),
            x => x.Name,
            false,
            cancellationToken);

        return  _mapper.Map<IReadOnlyCollection<GetCityDto>>(cities);

    }
}
