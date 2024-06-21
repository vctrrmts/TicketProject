using AutoFixture;
using Core.Tests;
using Core.Tests.Attributes;
using FluentValidation;
using TicketEventSearch.Application.Handlers.Seats.Queries.GetListSeats;
using Xunit.Abstractions;

namespace SearchService.UnitTests.Tests.Seats.Queries.GetListSeats;

public class GetListSeatsQueryValidatorTest : ValidatorTestBase<GetListSeatsQuery>
{
    public GetListSeatsQueryValidatorTest(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
    {
    }

    protected override IValidator<GetListSeatsQuery> TestValidator 
        => TestFixture.Create<GetListSeatsQueryValidator>();

    [Fact]
    public void Should_BeValid_When_RequestIsValid()
    {
        // arrange
        var query = new GetListSeatsQuery
        {
            SchemeId = Guid.NewGuid(),
            Limit = 5,
            Offset = 10
        };

        // act & assert
        AssertValid(query);
    }

    [Fact]
    public void Should_BeValid_When_SchemeIdIsValid()
    {
        // arrange
        var query = new GetListSeatsQuery
        {
            SchemeId = Guid.NewGuid()
        };

        // act & assert
        AssertValid(query);
    }

    [Fact]
    public void Should_BeNotValid_When_SchemeIdIsEmpty()
    {
        // arrange
        var query = new GetListSeatsQuery
        {
            SchemeId = Guid.Empty
        };

        // act & assert
        AssertNotValid(query);
    }

    [Theory]
    [FixtureInlineAutoData(1)]
    [FixtureInlineAutoData(10)]
    public void Should_BeValid_When_LimitIsValid(int limit)
    {
        // arrange
        var query = new GetListSeatsQuery
        {
            SchemeId = Guid.NewGuid(),
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
        var query = new GetListSeatsQuery
        {
            SchemeId = Guid.NewGuid(),
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
        var query = new GetListSeatsQuery
        {
            SchemeId = Guid.NewGuid(),
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
        var query = new GetListSeatsQuery
        {
            SchemeId = Guid.NewGuid(),
            Offset = offset,
        };

        // act & assert
        AssertNotValid(query);
    }
}
