using AutoFixture;
using Core.Tests;
using Core.Tests.Attributes;
using FluentValidation;
using Xunit.Abstractions;
using TicketEventSearch.Application.Handlers.Categories.Commands.CreateCategory;


namespace SearchService.UnitTests.Tests.Categories.Commands.CreateCategory;

public class CreateCategoryCommandValidatorTest : ValidatorTestBase<CreateCategoryCommand>
{
    public CreateCategoryCommandValidatorTest(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
    {
    }

    protected override IValidator<CreateCategoryCommand> TestValidator 
        => TestFixture.Create<CreateCategoryCommandValidator>();

    [Fact]
    public void Should_BeValid_When_RequestIsValid()
    {
        // arrange
        var command = new CreateCategoryCommand
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
        var command = new CreateCategoryCommand
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
        var command = new CreateCategoryCommand
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
        var command = new CreateCategoryCommand
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
        var command = new CreateCategoryCommand
        {
            CategoryId = Guid.Empty,
            Name = "test",
        };

        // act & assert
        AssertNotValid(command);
    }
}
