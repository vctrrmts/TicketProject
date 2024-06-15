using AutoMapper;
using MediatR;
using Serilog;
using System.Text.Json;
using TicketEventManagement.Application.Abstractions.Persistence;
using TicketEventManagement.Application.DTOs;
using TicketEventManagement.Application.Exceptions;
using TicketEventManagement.Domain;

namespace TicketEventManagement.Application.Handlers.Tickets.Commands.CreateTickets;

internal class CreateTicketsCommandHandler : IRequestHandler<CreateTicketsCommand, IReadOnlyCollection<GetTicketDto>>
{
    private readonly IBaseRepository<Ticket> _tickets;
    private readonly IBaseRepository<Event> _events;
    private readonly IBaseRepository<Scheme> _schemes;

    private readonly IMapper _mapper;

    public CreateTicketsCommandHandler(IBaseRepository<Ticket> tickets, IBaseRepository<Event> events,
        IBaseRepository<Scheme> schemes, IMapper mapper)
    {
        _tickets = tickets;
        _events = events;
        _schemes = schemes;
        _mapper = mapper;
    }

    public async Task<IReadOnlyCollection<GetTicketDto>> Handle(CreateTicketsCommand request, CancellationToken cancellationToken)
    {
        Event? eventOwner = await _events.SingleOrDefaultAsync(x => x.EventId == request.EventId);
        if (eventOwner is null)
        {
            throw new NotFoundException($"Event with EventId = {request.EventId} not found");
        }

        Scheme? scheme = await _schemes.SingleOrDefaultAsync(x => x.SchemeId == request.SchemeId);
        if (scheme is null)
        {
            throw new NotFoundException($"Scheme with SchemeId = {request.SchemeId} not found");
        }

        if (!eventOwner.Location.Schemes.Contains(scheme))
        {
            throw new BadOperationException($"Scheme with SchemeId = {request.SchemeId} does not belong to event with EventId = {request.EventId}");
        }

        Seat[] seatArray = scheme.Seats.ToArray();
        Ticket[] newTickets = new Ticket[seatArray.Length];

        for (int i = 0; i < seatArray.Length; i++)
        {
            newTickets[i] = new Ticket(request.EventId, seatArray[i].SeatId, request.Price);
        }
        newTickets = await _tickets.AddRangeAsync(newTickets, cancellationToken);
        Log.Information("Range of tickets by scheme added " + JsonSerializer.Serialize(request));

        return _mapper.Map<IReadOnlyCollection<GetTicketDto>>(newTickets);
    }
}
