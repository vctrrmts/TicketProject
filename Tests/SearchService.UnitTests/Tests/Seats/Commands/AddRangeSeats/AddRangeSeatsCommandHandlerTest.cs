using Core.Tests;
using System.Linq.Expressions;
using Moq;
using TicketEventSearch.Application.DTOs;
using TicketEventSearch.Application.Abstractions.Persistence;
using TicketEventSearch.Domain;
using AutoMapper;
using Core.Tests.Fixtures;
using Xunit.Abstractions;
using MediatR;
using AutoFixture;
using TicketEventSearch.Application.Handlers.Seats.Commands.AddRangeSeats;
using TicketEventSearch.Application.Caches.Seat;

namespace SearchService.UnitTests.Tests.Seats.Commands.AddRangeSeats;

public class AddRangeSeatsCommandHandlerTest : RequestHandlerTestBase<AddRangeSeatsCommand, IReadOnlyCollection<GetSeatDto>>
{
    private readonly Mock<IBaseRepository<Seat>> _seatsMock = new();
    private readonly Mock<IBaseRepository<Scheme>> _schemesMock = new();
    private readonly IMapper _mapper;
    private readonly Mock<ICleanSeatCacheService> _cleanSeatCacheService = new();

    public AddRangeSeatsCommandHandlerTest(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
    {
        _mapper = new AutoMapperFixture(typeof(GetSeatDto).Assembly).Mapper;
    }

    protected override IRequestHandler<AddRangeSeatsCommand, IReadOnlyCollection<GetSeatDto>> CommandHandler
        => new AddRangeSeatsCommandHandler(_seatsMock.Object, _schemesMock.Object, _mapper, _cleanSeatCacheService.Object);

    [Fact]
    public async Task Should_ThrowNotFound_When_SchemeNotFound()
    {
        // arrange
        var seats = new SeatForExportDto[10];
        for (int i = 0; i < 10; i++)
        {
            seats[i] = new SeatForExportDto()
            {
                SchemeId = Guid.NewGuid(),
                SeatId = Guid.NewGuid(),
                Sector = "sector",
                Row = 1,
                SeatNumber = i + 1
            };
        }

        var command = new AddRangeSeatsCommand() { Seats = seats };

        var schemeIds = command.Seats.Select(x => x.SchemeId).Distinct().ToArray();
        foreach (var schemeId in schemeIds)
        {
            _schemesMock.Setup(p => p.SingleOrDefaultAsync
            (It.IsAny<Expression<Func<Scheme, bool>>>(), default)).ReturnsAsync(null as Scheme);
        }

        // act and assert
        await AssertThrowNotFound(command);
    }

    [Fact]
    public async Task Should_BeValid_When_SchemesFound()
    {
        // arrange
        var seats = new SeatForExportDto[10];
        for (int i = 0; i < 10; i++)
        {
            seats[i] = new SeatForExportDto()
            {
                SchemeId = Guid.NewGuid(),
                SeatId = Guid.NewGuid(),
                Sector = "sector",
                Row = 1,
                SeatNumber = i + 1
            };
        }

        var command = new AddRangeSeatsCommand() { Seats = seats };

        var schemeIds = command.Seats.Select(x => x.SchemeId).Distinct().ToArray();
        foreach (var schemeId in schemeIds)
        {
            var scheme = TestFixture.Build<Scheme>().Create();
            _schemesMock.Setup(p => p.SingleOrDefaultAsync
            (It.IsAny<Expression<Func<Scheme, bool>>>(), default)).ReturnsAsync(scheme);
        }

        Seat[] newSeats = new Seat[command.Seats.Length];

        for (int i = 0; i < command.Seats.Length; i++)
        {
            newSeats[i] = new Seat(command.Seats[i].SeatId, command.Seats[i].SchemeId,
                command.Seats[i].Sector, command.Seats[i].Row, command.Seats[i].SeatNumber);
        }

        _seatsMock.Setup(p => p.AddRangeAsync(newSeats, default)).ReturnsAsync(newSeats);

        _cleanSeatCacheService.Object.ClearAllSeatCaches();

        // act and assert
        await AssertNotThrow(command);
    }
}
