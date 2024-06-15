using AutoMapper;
using MediatR;
using Serilog;
using System.Text.Json;
using TicketEventManagement.Application.Abstractions.Persistence;
using TicketEventManagement.Application.Abstractions.Service;
using TicketEventManagement.Application.DTOs;
using TicketEventManagement.Application.Exceptions;
using TicketEventManagement.Application.ExternalRepositories;
using TicketEventManagement.Domain;

namespace TicketEventManagement.Application.Handlers.Seats.Commands.DeleteRangeSeats;

public class DeleteRangeSeatsCommandHandler : IRequestHandler<DeleteRangeSeatsCommand>
{
    private readonly IBaseRepository<Seat> _seats;
    private readonly IBaseRepository<Scheme> _schemes;

    private readonly ISeatsRepository _seatsRepository;

    private readonly IMapper _mapper;

    private readonly ICurrentUserService _currentUserService;

    public DeleteRangeSeatsCommandHandler(IBaseRepository<Seat> seats, IBaseRepository<Scheme> schemes,
        ISeatsRepository seatsRepository, IMapper mapper, ICurrentUserService currentUserService)
    {
        _seats = seats;
        _schemes = schemes;
        _seatsRepository = seatsRepository;
        _mapper = mapper;
        _currentUserService = currentUserService;
    }

    public async Task Handle(DeleteRangeSeatsCommand request, CancellationToken cancellationToken)
    {
        Scheme? scheme = await _schemes.SingleOrDefaultAsync(x => x.SchemeId == request.SchemeId, cancellationToken);
        if (scheme == null)
        {
            throw new NotFoundException($"Scheme with SchemeId = {request.SchemeId} not found");
        }

        var seats = await _seats.GetListAsync(
            null,
            null,
            e => (e.SchemeId == request.SchemeId)
            && (e.Sector == request.Sector)
            && ((request.RowNumberStart == null || request.RowNumberEnd == null) 
            || (e.Row >= request.RowNumberStart && e.Row <= request.RowNumberEnd))
            && ((request.SeatNumberStart == null || request.SeatNumberEnd == null) 
            || (e.SeatNumber >= request.SeatNumberStart && e.SeatNumber <= request.SeatNumberEnd)),
            null,
            false,
            cancellationToken);

        await _seats.DeleteRangeAsync(seats, cancellationToken);

        string accessToken = _currentUserService.AccessToken;
        await _seatsRepository.RemoveRangeOfSeatsAsync(
                _mapper.Map<ICollection<SeatForExportDto>>(seats).ToArray(), accessToken, cancellationToken);

        Log.Information("Range of seats deleted " + JsonSerializer.Serialize(request));
    }
}
