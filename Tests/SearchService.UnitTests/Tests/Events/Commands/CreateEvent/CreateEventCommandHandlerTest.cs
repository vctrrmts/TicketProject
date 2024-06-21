using Core.Tests;
using Moq;
using TicketEventSearch.Application.Abstractions.Persistence;
using TicketEventSearch.Domain;
using Xunit.Abstractions;
using MediatR;
using Core.Tests.Attributes;
using TicketEventSearch.Application.Caches.Event;
using TicketEventSearch.Application.Handlers.Events.Commands.CreateEvent;

namespace SearchService.UnitTests.Tests.Events.Commands.CreateEvent;

public class CreateEventCommandHandlerTest : RequestHandlerTestBase<CreateEventCommand, bool>
{
    private readonly Mock<IBaseRepository<Event>> _eventsMock = new();
    private readonly Mock<IBaseRepository<Ticket>> _ticketsMock = new();
    private readonly Mock<ICleanEventCacheService> _cleanEventCacheService = new();
    public CreateEventCommandHandlerTest(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
    {
    }

    protected override IRequestHandler<CreateEventCommand, bool> CommandHandler 
        => new CreateEventCommandHandler(_eventsMock.Object, _ticketsMock.Object, _cleanEventCacheService.Object);

    [Theory, FixtureInlineAutoData]
    public async Task Should_BeValid(CreateEventCommand command)
    {
        // arrange
        command.UriMainImage = "string.com";
        command.DateTimeEventStart = DateTime.Now.AddDays(1);
        command.DateTimeEventEnd = DateTime.Now.AddDays(1).AddHours(2);
        var myEvent = new Event(command.EventId, Guid.NewGuid(), Guid.NewGuid(), command.Name, command.Description,
            command.UriMainImage, command.DateTimeEventStart, command.DateTimeEventEnd, command.IsActive);

        _eventsMock.Setup(p => p.AddAsync(myEvent, default)).ReturnsAsync(myEvent);

        var ticketArray = command.Tickets.ToArray();
        Ticket[] tickets = new Ticket[ticketArray.Length];
        for (int i = 0; i < ticketArray.Length; i++)
        {
            tickets[i] = new Ticket(ticketArray[i].TicketId, ticketArray[i].EventId,
                ticketArray[i].SeatId, ticketArray[i].Price);
        }

        _ticketsMock.Setup(p => p.AddRangeAsync(tickets, default)).ReturnsAsync(tickets);

        _cleanEventCacheService.Object.ClearListEventCaches();

        // act and assert
        await AssertNotThrow(command);
    }
}
