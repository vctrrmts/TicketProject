using AutoFixture;
using Core.Tests;
using Core.Tests.Attributes;
using FluentValidation;
using TicketEventSearch.Application.Handlers.Locations.Commands.CreateLocation;
using Xunit.Abstractions;

namespace SearchService.UnitTests.Tests.Locations.Commands.CreateLocation;

public class CreateLocationCommandValidatorTest : ValidatorTestBase<CreateLocationCommand>
{
    public CreateLocationCommandValidatorTest(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
    {
    }

    protected override IValidator<CreateLocationCommand> TestValidator 
        => TestFixture.Create<CreateLocationCommandValidator>();

    [Fact]
    public void Should_BeValid_When_RequestIsValid()
    {
        // arrange
        var command = new CreateLocationCommand
        {
            LocationId = Guid.NewGuid(),
            CityId = Guid.NewGuid(),
            Name = "test",
            Address = "test",
            Latitude = 0,
            Longitude = 0,
            IsActive = true
        };

        // act & assert
        AssertValid(command);
    }

    [Fact]
    public void Should_BeNotValid_When_LocationIdIsEmpty()
    {
        // arrange
        var command = new CreateLocationCommand
        {
            LocationId = Guid.Empty,
            CityId = Guid.NewGuid(),
            Name = "test",
            Address = "test",
            Latitude = 0,
            Longitude = 0,
            IsActive = true
        };

        // act & assert
        AssertValid(command);
    }

    [Fact]
    public void Should_BeNotValid_When_CityIdIsEmpty()
    {
        // arrange
        var command = new CreateLocationCommand
        {
            LocationId = Guid.NewGuid(),
            CityId = Guid.Empty,
            Name = "test",
            Address = "test",
            Latitude = 0,
            Longitude = 0,
            IsActive = true
        };

        // act & assert
        AssertNotValid(command);
    }

    [Theory]
    [FixtureInlineAutoData("1")]
    [FixtureInlineAutoData("12345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890")]
    public void Should_BeValid_When_NameIsValid(string name)
    {
        // arrange
        var command = new CreateLocationCommand
        {
            LocationId = Guid.NewGuid(),
            CityId = Guid.NewGuid(),
            Name = name,
            Address = "test",
            Latitude = 0,
            Longitude = 0,
            IsActive = true
        };

        // act & assert
        AssertValid(command);
    }

    [Theory]
    [FixtureInlineAutoData("")]
    [FixtureInlineAutoData("   ")]
    [FixtureInlineAutoData(null)]
    [FixtureInlineAutoData("123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901")]
    public void Should_NotBeValid_When_NameIsNotValid(string name)
    {
        // arrange
        var command = new CreateLocationCommand
        {
            LocationId = Guid.NewGuid(),
            CityId = Guid.NewGuid(),
            Name = name,
            Address = "test",
            Latitude = 0,
            Longitude = 0,
            IsActive = true
        };

        // act & assert
        AssertNotValid(command);
    }

    [Theory]
    [FixtureInlineAutoData(-90)]
    [FixtureInlineAutoData(0)]
    [FixtureInlineAutoData(90)]
    public void Should_BeValid_When_LatitudeIsValid(double latitude)
    {
        // arrange
        var command = new CreateLocationCommand
        {
            LocationId = Guid.NewGuid(),
            CityId = Guid.NewGuid(),
            Name = "name",
            Address = "test",
            Latitude = latitude,
            Longitude = 0,
            IsActive = true
        };

        // act & assert
        AssertValid(command);
    }

    [Theory]
    [FixtureInlineAutoData(-90.1)]
    [FixtureInlineAutoData(90.1)]
    public void Should_NotBeValid_When_LatitudeIsNotValid(double latitude)
    {
        // arrange
        var command = new CreateLocationCommand
        {
            LocationId = Guid.NewGuid(),
            CityId = Guid.NewGuid(),
            Name = "name",
            Address = "test",
            Latitude = latitude,
            Longitude = 0,
            IsActive = true
        };

        // act & assert
        AssertNotValid(command);
    }

    [Theory]
    [FixtureInlineAutoData(-90)]
    [FixtureInlineAutoData(0)]
    [FixtureInlineAutoData(90)]
    public void Should_BeValid_When_LongitudeIsValid(double longitude)
    {
        // arrange
        var command = new CreateLocationCommand
        {
            LocationId = Guid.NewGuid(),
            CityId = Guid.NewGuid(),
            Name = "name",
            Address = "test",
            Latitude = 0,
            Longitude = longitude,
            IsActive = true
        };

        // act & assert
        AssertValid(command);
    }

    [Theory]
    [FixtureInlineAutoData(-180.1)]
    [FixtureInlineAutoData(180.1)]
    public void Should_NotBeValid_When_LongitudeIsNotValid(double longitude)
    {
        // arrange
        var command = new CreateLocationCommand
        {
            LocationId = Guid.NewGuid(),
            CityId = Guid.NewGuid(),
            Name = "name",
            Address = "test",
            Latitude = 0,
            Longitude = longitude,
            IsActive = true
        };

        // act & assert
        AssertNotValid(command);
    }

}
