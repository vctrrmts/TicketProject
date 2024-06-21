using AutoFixture;
using Core.Tests;
using Core.Tests.Attributes;
using FluentValidation;
using TicketEventSearch.Application.Handlers.Categories.Queries.GetListCategories;
using Xunit.Abstractions;

namespace SearchService.UnitTests.Tests.Categories.Queries.GetListCategories;

public class GetListCategoriesQueryValidatorTest : ValidatorTestBase<GetListCategoriesQuery>
{
    public GetListCategoriesQueryValidatorTest(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
    {
    }

    protected override IValidator<GetListCategoriesQuery> TestValidator 
        => TestFixture.Create<GetListCategoriesQueryValidator>();

    [Fact]
    public void Should_BeValid_When_RequestIsValid()
    {
        // arrange
        var query = new GetListCategoriesQuery
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
        var query = new GetListCategoriesQuery
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
        var query = new GetListCategoriesQuery
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
        var query = new GetListCategoriesQuery
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
        var query = new GetListCategoriesQuery
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
        var query = new GetListCategoriesQuery
        {
            FreeText = freeText,
        };

        // act & assert
        AssertValid(query);
    }

    [Fact]
    public void Should_NotBeValid_When_FreeTextIsGreatThen100()
    {
        // arrange
        var query = new GetListCategoriesQuery
        {
            FreeText = "12345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901",
        };

        // act & assert
        AssertNotValid(query);
    }
}
