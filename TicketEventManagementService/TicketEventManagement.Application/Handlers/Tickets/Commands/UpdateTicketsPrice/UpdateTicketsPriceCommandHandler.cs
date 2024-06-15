using AutoMapper;
using MediatR;
using Serilog;
using System.Text.Json;
using TicketEventManagement.Application.Abstractions.Persistence;
using TicketEventManagement.Application.DTOs;
using TicketEventManagement.Application.Exceptions;
using TicketEventManagement.Domain;

namespace TicketEventManagement.Application.Handlers.Tickets.Commands.UpdateTicketsPrice;

public class UpdateTicketsPriceCommandHandler : IRequestHandler<UpdateTicketsPriceCommand, IReadOnlyCollection<GetTicketDto>>
{
    private readonly IBaseRepository<Ticket> _tickets;
    private readonly IBaseRepository<Event> _events;

    private readonly IMapper _mapper;

    public UpdateTicketsPriceCommandHandler(IBaseRepository<Ticket> tickets, 
        IBaseRepository<Event> events, IMapper mapper)
    {
        _tickets = tickets;
        _events = events;
        _mapper = mapper;
    }

    public async Task<IReadOnlyCollection<GetTicketDto>> Handle(UpdateTicketsPriceCommand request, 
        CancellationToken cancellationToken)
    {
        Event? eventOwner = await _events.SingleOrDefaultAsync(x => x.EventId == request.EventId);
        if (eventOwner is null)
        {
            throw new NotFoundException($"Event with EventId = {request.EventId} not found");
        }

        var tickets = await _tickets.GetListAsync(
            null,
            null,
            e => (e.EventId == request.EventId)
            && (e.Seat.Sector == request.Sector)
            && ((request.RowNumberStart == null || request.RowNumberEnd == null)
            || (e.Seat.Row >= request.RowNumberStart && e.Seat.Row <= request.RowNumberEnd))
            && ((request.SeatNumberStart == null || request.SeatNumberEnd == null)
            || (e.Seat.SeatNumber >= request.SeatNumberStart && e.Seat.SeatNumber <= request.SeatNumberEnd)),
            null,
            false,
            cancellationToken);

        foreach (var ticket in tickets) 
        {
            ticket.UpdatePrice(request.Price);
        }

        tickets = await _tickets.UpdateRangeAsync(tickets, cancellationToken);
        Log.Information("Range of ticket's price updated " + JsonSerializer.Serialize(request));

        return _mapper.Map<IReadOnlyCollection<GetTicketDto>>(tickets);
    }
}
