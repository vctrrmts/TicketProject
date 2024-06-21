using AutoFixture;
using Core.Tests;
using FluentValidation;
using TicketEventSearch.Application.DTOs;
using TicketEventSearch.Application.Handlers.Seats.Commands.DeleteRangeSeats;
using Xunit.Abstractions;

namespace SearchService.UnitTests.Tests.Seats.Commands.DeleteRangeSeats;

public class DeleteRangeSeatsCommandValidatorTest : ValidatorTestBase<DeleteRangeSeatsCommand>
{
    public DeleteRangeSeatsCommandValidatorTest(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
    {
    }

    protected override IValidator<DeleteRangeSeatsCommand> TestValidator 
        => TestFixture.Create<DeleteRangeSeatsCommandValidator>();

    [Fact]
    public void Should_BeValid_When_RequestIsValid()
    {
        // arrange
        var command = new DeleteRangeSeatsCommand
        {
            Seats = [Guid.NewGuid()]
        };

        // act & assert
        AssertValid(command);
    }

    [Fact]
    public void Should_BeNotValid_When_SeatsIsEmpty()
    {
        // arrange
        var seats = new Guid[0];

        var command = new DeleteRangeSeatsCommand
        {
            Seats = seats
        };

        // act & assert
        AssertNotValid(command);
    }
}
