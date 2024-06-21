using AutoFixture;
using Core.Tests;
using FluentValidation;
using TicketEventSearch.Application.Handlers.Locations.Commands.DeleteLocation;
using Xunit.Abstractions;

namespace SearchService.UnitTests.Tests.Locations.Commands.DeleteLocation;

public class DeleteLocationCommandValidatorTest : ValidatorTestBase<DeleteLocationCommand>
{
    public DeleteLocationCommandValidatorTest(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
    {
    }

    protected override IValidator<DeleteLocationCommand> TestValidator 
        => TestFixture.Create<DeleteLocationCommandValidator>();

    [Fact]
    public void Should_BeValid_When_LocationIdIsValid()
    {
        // arrange
        var command = new DeleteLocationCommand
        {
            LocationId = Guid.NewGuid()
        };

        // act & assert
        AssertValid(command);
    }

    [Fact]
    public void Should_BeNotValid_When_LocationIdIsNotValid()
    {
        // arrange
        var command = new DeleteLocationCommand
        {
            LocationId = Guid.Empty
        };

        // act & assert
        AssertNotValid(command);
    }
}
