using AutoFixture;
using Core.Tests;
using FluentValidation;
using TicketEventSearch.Application.Handlers.Cities.Commands.DeleteCity;
using Xunit.Abstractions;

namespace SearchService.UnitTests.Tests.Cities.Commands.DeleteCity;

public class DeleteCityCommandValidatorTest : ValidatorTestBase<DeleteCityCommand>
{
    public DeleteCityCommandValidatorTest(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
    {
    }

    protected override IValidator<DeleteCityCommand> TestValidator 
        => TestFixture.Create<DeleteCityCommandValidator>();

    [Fact]
    public void Should_BeValid_When_CityIdIsValid()
    {
        // arrange
        var command = new DeleteCityCommand
        {
            CityId = Guid.NewGuid()
        };

        // act & assert
        AssertValid(command);
    }

    [Fact]
    public void Should_BeNotValid_When_CityIdIsNotValid()
    {
        // arrange
        var command = new DeleteCityCommand
        {
            CityId = Guid.Empty
        };

        // act & assert
        AssertNotValid(command);
    }
}
