using AutoFixture;
using Core.Tests;
using FluentValidation;
using TicketEventSearch.Application.DTOs;
using TicketEventSearch.Application.Handlers.Seats.Commands.AddRangeSeats;
using Xunit.Abstractions;

namespace SearchService.UnitTests.Tests.Seats.Commands.AddRangeSeats;

public class AddRangeSeatsCommandValidatorTest : ValidatorTestBase<AddRangeSeatsCommand>
{
    public AddRangeSeatsCommandValidatorTest(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
    {
    }

    protected override IValidator<AddRangeSeatsCommand> TestValidator 
        => TestFixture.Create<AddRangeSeatsCommandValidator>();

    [Fact]
    public void Should_BeValid_When_RequestIsValid()
    {
        // arrange
        var seats = new SeatForExportDto[1] { new SeatForExportDto()
        {
            SchemeId = Guid.NewGuid(),
            SeatId = Guid.NewGuid(),
            Sector = "sector",
            Row = 1,
            SeatNumber = 1
        } };

        var command = new AddRangeSeatsCommand
        {
            Seats = seats
        };

        // act & assert
        AssertValid(command);
    }

    [Fact]
    public void Should_BeNotValid_When_SeatsIsEmpty()
    {
        // arrange
        var seats = new SeatForExportDto[0];

        var command = new AddRangeSeatsCommand
        {
            Seats = seats
        };

        // act & assert
        AssertNotValid(command);
    }
}
