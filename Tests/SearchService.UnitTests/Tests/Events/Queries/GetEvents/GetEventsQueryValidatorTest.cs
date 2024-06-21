using AutoFixture;
using Core.Tests;
using Core.Tests.Attributes;
using FluentValidation;
using TicketEventSearch.Application.Handlers.Events.Queries.GetEvents;
using Xunit.Abstractions;

namespace SearchService.UnitTests.Tests.Events.Queries.GetEvents;

public class GetEventsQueryValidatorTest : ValidatorTestBase<GetEventsQuery>
{
    public GetEventsQueryValidatorTest(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
    {
    }

    protected override IValidator<GetEventsQuery> TestValidator 
        => TestFixture.Create<GetEventsQueryValidator>();

    [Fact]
    public void Should_BeValid_When_RequestIsValid()
    {
        // arrange
        var query = new GetEventsQuery
        {
            CategoryId = Guid.NewGuid(),
            LocationId = Guid.NewGuid(),
            Limit = 5,
            Offset = 10
        };

        // act & assert
        AssertValid(query);
    }

    [Fact]
    public void Should_BeNotValid_When_CategoryIdIsEmpty()
    {
        // arrange
        var query = new GetEventsQuery
        {
            CategoryId = Guid.Empty
        };

        // act & assert
        AssertValid(query);
    }

    [Fact]
    public void Should_BeNotValid_When_LocationIdIsEmpty()
    {
        // arrange
        var query = new GetEventsQuery
        {
            LocationId = Guid.Empty
        };

        // act & assert
        AssertValid(query);
    }

    [Theory]
    [FixtureInlineAutoData(1)]
    [FixtureInlineAutoData(10)]
    public void Should_BeValid_When_LimitIsValid(int limit)
    {
        // arrange
        var query = new GetEventsQuery
        {
            Limit = limit,
        };

        // act & assert
        AssertValid(query);
    }

    [Theory]
    [FixtureInlineAutoData(0)]
    [FixtureInlineAutoData(-1)]
    public void Should_NotBeValid_When_IncorrectLimit(int limit)
    {
        // arrange
        var query = new GetEventsQuery
        {
            Limit = limit,
        };

        // act & assert
        AssertNotValid(query);
    }

    [Theory]
    [FixtureInlineAutoData(1)]
    [FixtureInlineAutoData(10)]
    [FixtureInlineAutoData(20)]
    public void Should_BeValid_When_OffsetIsValid(int offset)
    {
        // arrange
        var query = new GetEventsQuery
        {
            Offset = offset,
        };

        // act & assert
        AssertValid(query);
    }

    [Theory]
    [FixtureInlineAutoData(0)]
    [FixtureInlineAutoData(-1)]
    public void Should_NotBeValid_When_IncorrectOffset(int offset)
    {
        // arrange
        var query = new GetEventsQuery
        {
            Offset = offset,
        };

        // act & assert
        AssertNotValid(query);
    }
}
