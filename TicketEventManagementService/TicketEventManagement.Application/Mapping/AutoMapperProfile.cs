using AutoMapper;
using TicketEventManagement.Application.DTOs;
using TicketEventManagement.Domain;

namespace TicketEventManagement.Application.Mapping;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<Event, GetEventDto>();
        CreateMap<Event, EventForExportDto>();
        CreateMap<Ticket, GetTicketDto>();
        CreateMap<Ticket, TicketForExportDto>();
        CreateMap<City, GetCityDto>();
        CreateMap<Category, GetCategoryDto>();
        CreateMap<Category, GetCategoryForEventDto>();
        CreateMap<Location, GetLocationDto>();
        CreateMap<Location, GetLocationForEventDto>();
        CreateMap<Scheme, GetSchemeDto>();
        CreateMap<Seat, GetSeatDto>();
        CreateMap<Seat, SeatForExportDto>();
    }

}
