using AutoFixture;
using Core.Tests;
using Core.Tests.Attributes;
using FluentValidation;
using TicketEventSearch.Application.Handlers.Events.Queries.GetEventById;
using Xunit.Abstractions;

namespace SearchService.UnitTests.Tests.Events.Queries.GetEventById;

public class GetEventByIdQueryValidatorTest : ValidatorTestBase<GetEventByIdQuery>
{
    public GetEventByIdQueryValidatorTest(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
    {
    }

    protected override IValidator<GetEventByIdQuery> TestValidator 
        => TestFixture.Create<GetEventByIdQueryValidator>();

    [Fact]
    public void Should_BeValid_When_EventIdIsValid()
    {
        // arrange
        var query = new GetEventByIdQuery
        {
            EventId = Guid.NewGuid()
        };

        // act & assert
        AssertValid(query);
    }

    [Fact]
    public void Should_BeNotValid_When_EventIdIsNotValid()
    {
        // arrange
        var query = new GetEventByIdQuery
        {
            EventId = Guid.Empty
        };

        // act & assert
        AssertNotValid(query);
    }
}
