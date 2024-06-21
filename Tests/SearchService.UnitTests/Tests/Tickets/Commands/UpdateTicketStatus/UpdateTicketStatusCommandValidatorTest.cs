using AutoFixture;
using Core.Tests;
using Core.Tests.Attributes;
using FluentValidation;
using TicketEventSearch.Application.Handlers.Tickets.Commands.UpdateTicketStatus;
using Xunit.Abstractions;

namespace SearchService.UnitTests.Tests.Tickets.Commands.UpdateTicketStatus;

public class UpdateTicketStatusCommandValidatorTest : ValidatorTestBase<UpdateTicketStatusCommand>
{
    public UpdateTicketStatusCommandValidatorTest(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
    {
    }

    protected override IValidator<UpdateTicketStatusCommand> TestValidator 
        => TestFixture.Create<UpdateTicketStatusCommandValidator>();

    [Fact]
    public void Should_BeValid_When_RequestIsValid()
    {
        // arrange
        var command = new UpdateTicketStatusCommand
        {
            TicketId = Guid.NewGuid(),
            TicketStatusId = 1
        };

        // act & assert
        AssertValid(command);
    }

    [Theory]
    [FixtureInlineAutoData(1)]
    [FixtureInlineAutoData(2)]
    [FixtureInlineAutoData(3)]
    public void Should_BeValid_When_TicketStatusIsValid(int ticketStatus)
    {
        // arrange
        var command = new UpdateTicketStatusCommand
        {
            TicketId = Guid.NewGuid(),
            TicketStatusId = ticketStatus
        };

        // act & assert
        AssertValid(command);
    }

    [Theory]
    [FixtureInlineAutoData(-1)]
    [FixtureInlineAutoData(0)]
    [FixtureInlineAutoData(4)]
    public void Should_NotBeValid_When_TicketStatusIsNotValid(int ticketStatus)
    {
        // arrange
        var command = new UpdateTicketStatusCommand
        {
            TicketId = Guid.NewGuid(),
            TicketStatusId = ticketStatus
        };

        // act & assert
        AssertNotValid(command);
    }

    [Fact]
    public void Should_BeValid_When_TicketIdIsValid()
    {
        // arrange
        var command = new UpdateTicketStatusCommand
        {
            TicketId = Guid.NewGuid(),
            TicketStatusId = 1
        };

        // act & assert
        AssertValid(command);
    }

    [Fact]
    public void Should_BeNotValid_When_TicketIdIsEmpty()
    {
        // arrange
        var command = new UpdateTicketStatusCommand
        {
            TicketId = Guid.Empty,
            TicketStatusId = 1
        };

        // act & assert
        AssertNotValid(command);
    }
}
