using AutoFixture;
using Core.Tests;
using FluentValidation;
using TicketEventSearch.Application.Handlers.Categories.Commands.DeleteCategory;
using Xunit.Abstractions;

namespace SearchService.UnitTests.Tests.Categories.Commands.DeleteCategory;

public class DeleteCategoryCommandValidatorTest : ValidatorTestBase<DeleteCategoryCommand>
{
    public DeleteCategoryCommandValidatorTest(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
    {
    }

    protected override IValidator<DeleteCategoryCommand> TestValidator 
        => TestFixture.Create<DeleteCategoryCommandValidator>();

    [Fact]
    public void Should_BeValid_When_CategoryIdIsValid()
    {
        // arrange
        var command = new DeleteCategoryCommand
        {
            CategoryId = Guid.NewGuid()
        };

        // act & assert
        AssertValid(command);
    }

    [Fact]
    public void Should_BeNotValid_When_CategoryIdIsNotValid()
    {
        // arrange
        var command = new DeleteCategoryCommand
        {
            CategoryId = Guid.Empty
        };

        // act & assert
        AssertNotValid(command);
    }
}
