using AutoFixture;
using Core.Tests;
using Core.Tests.Attributes;
using FluentValidation;
using TicketEventSearch.Application.Handlers.Events.Commands.UpdateEvent;
using Xunit.Abstractions;

namespace SearchService.UnitTests.Tests.Events.Commands.UpdateEvent;

public class UpdateEventCommandValidatorTest : ValidatorTestBase<UpdateEventCommand>
{
    public UpdateEventCommandValidatorTest(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
    {
    }

    protected override IValidator<UpdateEventCommand> TestValidator 
        => TestFixture.Create<UpdateEventCommandValidator>();

    [Fact]
    public void Should_BeValid_When_RequestIsValid()
    {
        // arrange
        var command = new UpdateEventCommand
        {
            EventId = Guid.NewGuid(),
            Name = "test",
            Description = "test",
            UriMainImage = "string.com",
            DateTimeEventStart = DateTime.Now,
            DateTimeEventEnd = DateTime.Now,
            IsActive = true
        };

        // act & assert
        AssertValid(command);
    }

    [Fact]
    public void Should_BeNotValid_When_EventIdIsNotValid()
    {
        // arrange
        var command = new UpdateEventCommand
        {
            EventId = Guid.Empty,
            Name = "test",
            Description = "test",
            UriMainImage = "string.com",
            DateTimeEventStart = DateTime.Now,
            DateTimeEventEnd = DateTime.Now,
            IsActive = true
        };

        // act & assert
        AssertNotValid(command);
    }

    [Theory]
    [FixtureInlineAutoData("1")]
    [FixtureInlineAutoData("1234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890")]
    public void Should_BeValid_When_NameIsValid(string name)
    {
        // arrange
        var command = new UpdateEventCommand
        {
            EventId = Guid.NewGuid(),
            Name = name,
            Description = "test",
            UriMainImage = "string.com",
            DateTimeEventStart = DateTime.Now,
            DateTimeEventEnd = DateTime.Now,
            IsActive = true
        };

        // act & assert
        AssertValid(command);
    }

    [Theory]
    [FixtureInlineAutoData("")]
    [FixtureInlineAutoData("   ")]
    [FixtureInlineAutoData(null)]
    [FixtureInlineAutoData("12345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901")]
    public void Should_NotBeValid_When_NameIsNotValid(string name)
    {
        // arrange
        var command = new UpdateEventCommand
        {
            EventId = Guid.NewGuid(),
            Name = name,
            Description = "test",
            UriMainImage = "string.com",
            DateTimeEventStart = DateTime.Now,
            DateTimeEventEnd = DateTime.Now,
            IsActive = true
        };

        // act & assert
        AssertNotValid(command);
    }

    [Theory]
    [FixtureInlineAutoData("1")]
    [FixtureInlineAutoData("1234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890")]
    public void Should_BeValid_When_DescriptionIsValid(string description)
    {
        // arrange
        var command = new UpdateEventCommand
        {
            EventId = Guid.NewGuid(),
            Name = "name",
            Description = description,
            UriMainImage = "string.com",
            DateTimeEventStart = DateTime.Now,
            DateTimeEventEnd = DateTime.Now,
            IsActive = true
        };

        // act & assert
        AssertValid(command);
    }

    [Theory]
    [FixtureInlineAutoData("")]
    [FixtureInlineAutoData("   ")]
    [FixtureInlineAutoData(null)]
    [FixtureInlineAutoData("12345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901")]
    public void Should_NotBeValid_When_DescriptionIsNotValid(string description)
    {
        // arrange
        var command = new UpdateEventCommand
        {
            EventId = Guid.NewGuid(),
            Name = "name",
            Description = description,
            UriMainImage = "string.com",
            DateTimeEventStart = DateTime.Now,
            DateTimeEventEnd = DateTime.Now,
            IsActive = true
        };

        // act & assert
        AssertNotValid(command);
    }

    [Theory]
    [FixtureInlineAutoData("1")]
    [FixtureInlineAutoData("12345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890")]
    public void Should_BeValid_When_UriMainImageIsValid(string uri)
    {
        // arrange
        var command = new UpdateEventCommand
        {
            EventId = Guid.NewGuid(),
            Name = "name",
            Description = "test",
            UriMainImage = uri,
            DateTimeEventStart = DateTime.Now,
            DateTimeEventEnd = DateTime.Now,
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
    public void Should_NotBeValid_When_UriMainImageIsNotValid(string uri)
    {
        // arrange
        var command = new UpdateEventCommand
        {
            EventId = Guid.NewGuid(),
            Name = "name",
            Description = "test",
            UriMainImage = uri,
            DateTimeEventStart = DateTime.Now,
            DateTimeEventEnd = DateTime.Now,
            IsActive = true
        };

        // act & assert
        AssertNotValid(command);
    }
}
