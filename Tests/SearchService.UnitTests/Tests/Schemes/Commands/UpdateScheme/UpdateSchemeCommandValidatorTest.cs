using AutoFixture;
using Core.Tests;
using Core.Tests.Attributes;
using FluentValidation;
using Xunit.Abstractions;
using TicketEventSearch.Application.Handlers.Schemes.Commands.UpdateScheme;

namespace SearchService.UnitTests.Tests.Schemes.Commands.UpdateScheme;

public class UpdateSchemeCommandValidatorTest : ValidatorTestBase<UpdateSchemeCommand>
{
    public UpdateSchemeCommandValidatorTest(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
    {
    }

    protected override IValidator<UpdateSchemeCommand> TestValidator 
        => TestFixture.Create<UpdateSchemeCommandValidator>();

    [Fact]
    public void Should_BeValid_When_RequestIsValid()
    {
        // arrange
        var command = new UpdateSchemeCommand
        {
            SchemeId = Guid.NewGuid(),
            Name = "test"
        };

        // act & assert
        AssertValid(command);
    }

    [Fact]
    public void Should_BeNotValid_When_SchemeIdIsEmpty()
    {
        // arrange
        var command = new UpdateSchemeCommand
        {
            SchemeId = Guid.Empty,
            Name = "test"
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
        var command = new UpdateSchemeCommand
        {
            SchemeId = Guid.NewGuid(),
            Name = name
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
        var command = new UpdateSchemeCommand
        {
            SchemeId = Guid.NewGuid(),
            Name = name
        };

        // act & assert
        AssertNotValid(command);
    }
}
