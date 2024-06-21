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

public class UpdateTicketStatusCommandHandlerTest : RequestHandlerTestBase<UpdateTicketStatusCommand, GetTicketForSentMailDto>
{
    private readonly Mock<IBaseRepository<Ticket>> _ticketsMock = new();
    private readonly IMapper _mapper;
    private readonly Mock<ICleanTicketCacheService> _cleanTicketCacheService = new();

    public UpdateTicketStatusCommandHandlerTest(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
    {
        _mapper = new AutoMapperFixture(typeof(GetTicketForSentMailDto).Assembly).Mapper;
    }

    protected override IRequestHandler<UpdateTicketStatusCommand, GetTicketForSentMailDto> CommandHandler 
        => new UpdateTicketStatusCommandHandler(_ticketsMock.Object, _cleanTicketCacheService.Object, _mapper);

    [Theory, FixtureInlineAutoData]
    public async Task Should_BeValid(UpdateTicketStatusCommand command)
    {
        // arrange
        command.TicketStatusId = 2;
        var ticket = TestFixture.Build<Ticket>().Create();

        _ticketsMock.Setup(
            p => p.SingleOrDefaultAsync(It.IsAny<Expression<Func<Ticket, bool>>>(), default)
        ).ReturnsAsync(ticket);

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

        _cleanTicketCacheService.Object.ClearAllTicketCaches();

        // act and assert
        await AssertNotThrow(command);
    }

    [Theory, FixtureInlineAutoData]
    public async Task Should_ThrowNotFound_When_TicketNotFound(UpdateTicketStatusCommand command)
    {
        // arrange
        _ticketsMock.Setup(
            p => p.SingleOrDefaultAsync(It.IsAny<Expression<Func<Ticket, bool>>>(), default)
        ).ReturnsAsync(null as Ticket);

        //// act and assert
        await AssertThrowNotFound(command);
    }

    [Theory, FixtureInlineAutoData]
    public async Task Should_ThrowNotFound_When_TicketAlreadyOrdered(UpdateTicketStatusCommand command)
    {
        // arrange
        command.TicketStatusId = 2;
        var ticket = TestFixture.Build<Ticket>().Create();
        ticket.UpdateStatusId(3);

        _ticketsMock.Setup(
            p => p.SingleOrDefaultAsync(It.IsAny<Expression<Func<Ticket, bool>>>(), default)
        ).ReturnsAsync(ticket);

        //// act and assert
        await AssertThrowBadOperation(command, $"Ticket with TicketId = {command.TicketId} already ordered");
    }

}
