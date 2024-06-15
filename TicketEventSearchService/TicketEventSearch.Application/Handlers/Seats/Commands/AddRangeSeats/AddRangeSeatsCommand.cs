using MediatR;
using TicketEventSearch.Application.DTOs;

namespace TicketEventSearch.Application.Handlers.Seats.Commands.AddRangeSeats;

public class AddRangeSeatsCommand : IRequest<IReadOnlyCollection<GetSeatDto>>
{
    public SeatForExportDto[] Seats { get; set; } = default!;
}
