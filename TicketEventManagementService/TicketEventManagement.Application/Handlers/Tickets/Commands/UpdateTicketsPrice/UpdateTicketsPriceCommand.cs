using MediatR;
using TicketEventManagement.Application.DTOs;

namespace TicketEventManagement.Application.Handlers.Tickets.Commands.UpdateTicketsPrice;

public class UpdateTicketsPriceCommand : IRequest<IReadOnlyCollection<GetTicketDto>>
{
    public Guid EventId { get; set; } = default!;
    public string Sector { get; set; } = default!;
    public int? RowNumberStart { get; set; }
    public int? RowNumberEnd { get; set; }
    public int? SeatNumberStart { get; set; }
    public int? SeatNumberEnd { get; set; }
    public double Price { get; set; }
}
