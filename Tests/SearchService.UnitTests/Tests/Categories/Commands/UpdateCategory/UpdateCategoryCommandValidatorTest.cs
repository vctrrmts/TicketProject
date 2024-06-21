using AutoFixture;
using Core.Tests;
using Core.Tests.Attributes;
using FluentValidation;
using TicketEventSearch.Application.Handlers.Categories.Commands.UpdateCategory;
using Xunit.Abstractions;

namespace SearchService.UnitTests.Tests.Categories.Commands.UpdateCategory;

public class UpdateCategoryCommandValidatorTest : ValidatorTestBase<UpdateCategoryCommand>
{
    public UpdateCategoryCommandValidatorTest(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
    {
    }

    protected override IValidator<UpdateCategoryCommand> TestValidator => 
        TestFixture.Create<UpdateCategoryCommandValidator>();

    [Fact]
    public void Should_BeValid_When_RequestIsValid()
    {
        // arrange
        var command = new UpdateCategoryCommand
        {
            CategoryId = Guid.NewGuid(),
            Name = "test",
            IsActive = true
        };

        // act & assert
        AssertValid(command);
    }

    [Theory]
    [FixtureInlineAutoData("1")]
    [FixtureInlineAutoData("123456789012345678901234567890")]
    public void Should_BeValid_When_NameIsValid(string name)
    {
        // arrange
        var command = new UpdateCategoryCommand
        {
            CategoryId = Guid.NewGuid(),
            Name = name,
        };

        // act & assert
        AssertValid(command);
    }

    [Theory]
    [FixtureInlineAutoData("")]
    [FixtureInlineAutoData("   ")]
    [FixtureInlineAutoData(null)]
    [FixtureInlineAutoData("1234567890123456789012345678901")]
    public void Should_NotBeValid_When_NameIsNotValid(string name)
    {
        // arrange
        var command = new UpdateCategoryCommand
        {
            CategoryId = Guid.NewGuid(),
            Name = name,
        };

        // act & assert
        AssertNotValid(command);
    }

    [Fact]
    public void Should_BeValid_When_CategoryIdIsValid()
    {
        // arrange
        var command = new UpdateCategoryCommand
        {
            CategoryId = Guid.NewGuid(),
            Name = "test",
        };

        // act & assert
        AssertValid(command);
    }

    [Fact]
    public void Should_BeNotValid_When_CategoryIdIsNotValid()
    {
        // arrange
        var command = new UpdateCategoryCommand
        {
            CategoryId = Guid.Empty,
            Name = "test",
        };

        // act & assert
        AssertNotValid(command);
    }
}
