using AutoFixture;
using Core.Tests;
using Core.Tests.Attributes;
using FluentValidation;
using TicketEventSearch.Application.Handlers.Locations.Queries.GetListLocations;
using Xunit.Abstractions;

namespace SearchService.UnitTests.Tests.Locations.Queries.GetListLocations;

public class GetListLocationQueryValidatorTest : ValidatorTestBase<GetListLocationQuery>
{
    public GetListLocationQueryValidatorTest(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
    {
    }

    protected override IValidator<GetListLocationQuery> TestValidator 
        => TestFixture.Create<GetListLocationQueryValidator>();

    [Fact]
    public void Should_BeValid_When_RequestIsValid()
    {
        // arrange
        var query = new GetListLocationQuery
        {
            Limit = 5,
            Offset = 10,
            FreeText = "123"
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
        var query = new GetListLocationQuery
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
        var query = new GetListLocationQuery
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
        var query = new GetListLocationQuery
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
        var query = new GetListLocationQuery
        {
            Offset = offset,
        };

        // act & assert
        AssertNotValid(query);
    }

    [Theory]
    [FixtureInlineAutoData("")]
    [FixtureInlineAutoData(null)]
    [FixtureInlineAutoData("123")]
    [FixtureInlineAutoData("1#*&^%$#@#$%±~`}{][\\|?/.,<>")]
    [FixtureInlineAutoData("1234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890")]
    public void Should_BeValid_When_FreeTextIsValid(string freeText)
    {
        // arrange
        var query = new GetListLocationQuery
        {
            FreeText = freeText,
        };

        // act & assert
        AssertValid(query);
    }

    [Theory]
    [FixtureInlineAutoData("12345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901")]
    public void Should_NotBeValid_When_FreeTextIsGreatThen100(string freeText)
    {
        // arrange
        var query = new GetListLocationQuery
        {
            FreeText = freeText,
        };

        // act & assert
        AssertNotValid(query);
    }
}
