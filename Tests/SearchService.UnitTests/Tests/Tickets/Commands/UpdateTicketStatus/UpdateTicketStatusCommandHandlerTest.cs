using Moq;
using TicketEventSearch.Application.Abstractions.Persistence;
using TicketEventSearch.Domain;
using AutoMapper;
using Core.Tests.Fixtures;
using Xunit.Abstractions;
using MediatR;
using Core.Tests.Attributes;
using AutoFixture;
using Core.Tests;
using TicketEventSearch.Application.Handlers.Tickets.Commands.UpdateTicketStatus;
using TicketEventSearch.Application.Caches.Ticket;
using System.Linq.Expressions;
using TicketEventSearch.Domain.Enums;

namespace SearchService.UnitTests.Tests.Tickets.Commands.UpdateTicketStatus;

public class UpdateTicketStatusCommandHandlerTest : RequestHandlerTestBase<UpdateTicketStatusCommand, IReadOnlyCollection<GetTicketForSentMailDto>>
{
    private readonly Mock<IBaseRepository<Ticket>> _ticketsMock = new();
    private readonly IMapper _mapper;
    private readonly Mock<ICleanTicketCacheService> _cleanTicketCacheService = new();

    public UpdateTicketStatusCommandHandlerTest(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
    {
        _mapper = new AutoMapperFixture(typeof(GetTicketForSentMailDto).Assembly).Mapper;
    }

    protected override IRequestHandler<UpdateTicketStatusCommand, IReadOnlyCollection<GetTicketForSentMailDto>> CommandHandler 
        => new UpdateTicketStatusCommandHandler(_ticketsMock.Object, _cleanTicketCacheService.Object, _mapper);

    [Fact]
    public async Task Should_BeValid_WhenSetStatusUnavailable()
    {
        // arrange
        UpdateTicketStatusCommand command = new UpdateTicketStatusCommand()
        {
            TicketIds = new Guid[2],
            TicketStatusId = 2
        };

        var tickets = TestFixture.Build<Ticket>().CreateMany(2).ToArray();

        command.TicketIds[0] = tickets[0].TicketId;
        command.TicketIds[1] = tickets[1].TicketId;

        _ticketsMock.Setup(
            p => p.GetListAsync(default, default, It.IsAny<Expression<Func<Ticket, bool>>>(),
            default, default, default)
        ).ReturnsAsync(tickets);

        foreach (var ticket in tickets)
        {
            ticket.UpdateStatusId(command.TicketStatusId);

            if (command.TicketStatusId == (int)TicketStatusEnum.Unavailable)
            {
                ticket.UpdateUnavailableStatusEnd(DateTime.UtcNow.AddMinutes(10));
            }
            else
            {
                ticket.UpdateUnavailableStatusEnd(null);
            }

            _ticketsMock.Setup(p => p.UpdateAsync(ticket, default)).ReturnsAsync(ticket);
        }

        _cleanTicketCacheService.Object.ClearAllTicketCaches();

        // act and assert
        await AssertNotThrow(command);
    }

    [Fact]
    public async Task Should_BeValid_WhenSetStatusFree()
    {
        // arrange
        UpdateTicketStatusCommand command = new UpdateTicketStatusCommand()
        {
            TicketIds = new Guid[2],
            TicketStatusId = 1
        };

        var tickets = TestFixture.Build<Ticket>().CreateMany(2).ToArray();

        command.TicketIds[0] = tickets[0].TicketId;
        command.TicketIds[1] = tickets[1].TicketId;

        _ticketsMock.Setup(
            p => p.GetListAsync(default, default, It.IsAny<Expression<Func<Ticket, bool>>>(),
            default, default, default)
        ).ReturnsAsync(tickets);

        foreach (var ticket in tickets)
        {
            ticket.UpdateStatusId(command.TicketStatusId);

            if (command.TicketStatusId == (int)TicketStatusEnum.Unavailable)
            {
                ticket.UpdateUnavailableStatusEnd(DateTime.UtcNow.AddMinutes(10));
            }
            else
            {
                ticket.UpdateUnavailableStatusEnd(null);
            }

            _ticketsMock.Setup(p => p.UpdateAsync(ticket, default)).ReturnsAsync(ticket);
        }

        _cleanTicketCacheService.Object.ClearAllTicketCaches();

        // act and assert
        await AssertNotThrow(command);
    }

    [Theory, FixtureInlineAutoData]
    public async Task Should_ThrowNotFound_When_TicketNotFound(UpdateTicketStatusCommand command)
    {
        // arrange
        _ticketsMock.Setup(
                    p => p.GetListAsync(default, default, It.IsAny<Expression<Func<Ticket, bool>>>(),
                    default, default, default)
                ).ReturnsAsync(new Ticket[0]);

        //// act and assert
        await AssertThrowNotFound(command);
    }

    [Fact]
    public async Task Should_ThrowNotFound_When_TicketAlreadyOrdered()
    {
        // arrange
        UpdateTicketStatusCommand command = new UpdateTicketStatusCommand()
        {
            TicketIds = new Guid[1],
            TicketStatusId = 2
        };

        var tickets = TestFixture.Build<Ticket>().CreateMany(1).ToArray();
        command.TicketIds[0] = tickets[0].TicketId;
        tickets[0].UpdateStatusId(3);

        _ticketsMock.Setup(
            p => p.GetListAsync(default, default, It.IsAny<Expression<Func<Ticket, bool>>>(),
            default, default, default)
        ).ReturnsAsync(tickets);

        //// act and assert
        await AssertThrowBadOperation(command, $"Ticket with TicketId = {command.TicketIds[0]} already ordered");
    }

}
