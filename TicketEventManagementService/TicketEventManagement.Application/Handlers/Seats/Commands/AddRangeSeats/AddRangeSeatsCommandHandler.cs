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

namespace TicketEventManagement.Application.Handlers.Seats.Commands.AddRangeSeats;

public class AddRangeSeatsCommandHandler : IRequestHandler<AddRangeSeatsCommand, IReadOnlyCollection<GetSeatDto>>
{
    private readonly IBaseRepository<Seat> _seats;
    private readonly IBaseRepository<Scheme> _schemes;

    private readonly ISeatsRepository _seatsRepository;

    private readonly IMapper _mapper;

    private readonly ICurrentUserService _currentUserService;

    public AddRangeSeatsCommandHandler(IBaseRepository<Seat> seats, IBaseRepository<Scheme> schemes, 
        ISeatsRepository seatsRepository, IMapper mapper, ICurrentUserService currentUserService)
    {
        _seats = seats;
        _schemes = schemes;
        _seatsRepository = seatsRepository;
        _mapper = mapper;
        _currentUserService = currentUserService;
    }

    public async Task<IReadOnlyCollection<GetSeatDto>> Handle(AddRangeSeatsCommand request, CancellationToken cancellationToken)
    {
        Scheme? scheme = await _schemes.SingleOrDefaultAsync(x => x.SchemeId == request.SchemeId, cancellationToken);
        if (scheme == null)
        {
            throw new NotFoundException($"Scheme with SchemeId = {request.SchemeId} not found");
        }

        int countSeats;

        if (request.RowNumberStart is not null && request.RowNumberEnd is not null)
        {
            countSeats = (request.RowNumberEnd.Value - request.RowNumberStart.Value + 1)
                *(request.SeatNumberEnd - request.SeatNumberStart + 1);
        }
        else
        {
            countSeats = request.SeatNumberEnd - request.SeatNumberStart + 1;
        }

        Seat[] newSeats = new Seat[countSeats];

        int indexRow = 0;
        if (request.RowNumberStart is not null && request.RowNumberEnd is not null)
        { 
            for (int i = (int)request.RowNumberStart; i <= request.RowNumberEnd; i++)
            {
                for (int j = request.SeatNumberStart; j <= request.SeatNumberEnd; j++)
                {
                    newSeats[indexRow] = new Seat(request.SchemeId, request.Sector, i, j);
                    indexRow++;
                }
            }
        }
        else
        {
            for (int j = request.SeatNumberStart; j <= request.SeatNumberEnd; j++)
            {
                newSeats[indexRow] = new Seat(request.SchemeId, request.Sector, null, j);
                indexRow++;
            }
        }

        newSeats = await _seats.AddRangeAsync(newSeats.ToArray(), cancellationToken);

        string accessToken = _currentUserService.AccessToken;
        await _seatsRepository.AddRangeOfSeatsAsync(
            _mapper.Map<ICollection<SeatForExportDto>>(newSeats).ToArray(), accessToken, cancellationToken);

        Log.Information("Range of seats added " + JsonSerializer.Serialize(request));

        return _mapper.Map<IReadOnlyCollection<GetSeatDto>>(newSeats);
    }
}
