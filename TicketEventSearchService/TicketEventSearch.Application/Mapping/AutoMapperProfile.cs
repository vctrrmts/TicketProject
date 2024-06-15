using AutoMapper;
using TicketEventSearch.Application.DTOs;
using TicketEventSearch.Application.Handlers.Events.Commands.CreateEvent;
using TicketEventSearch.Application.Handlers.Tickets.Commands.UpdateTicketStatus;
using TicketEventSearch.Domain;

namespace TicketEventSearch.Application.Mapping;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<Event, GetEventDto>();
        CreateMap<Ticket, GetTicketDto>();
        CreateMap<Ticket, GetTicketForSentMailDto>();
        CreateMap<TicketForExportDto, Ticket>();
        CreateMap<City, GetCityDto>();
        CreateMap<Category, GetCategoryDto>();
        CreateMap<Category, GetCategoryForEventDto>();
        CreateMap<Location, GetLocationDto>();
        CreateMap<Location, GetLocationForEventDto>();
        CreateMap<Seat, GetSeatDto>();
        CreateMap<Seat, SeatForExportDto>();
    }

}
