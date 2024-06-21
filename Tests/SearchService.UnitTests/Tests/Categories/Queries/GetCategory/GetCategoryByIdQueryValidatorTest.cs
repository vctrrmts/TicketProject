using AutoFixture;
using Core.Tests;
using FluentValidation;
using Xunit.Abstractions;
using TicketEventSearch.Application.Handlers.Categories.Queries.GetCategory;


namespace SearchService.UnitTests.Tests.Categories.Queries.GetCategory;

public class GetCategoryByIdQueryValidatorTest : ValidatorTestBase<GetCategoryByIdQuery>
{
    public GetCategoryByIdQueryValidatorTest(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
    {
    }

    protected override IValidator<GetCategoryByIdQuery> TestValidator 
        => TestFixture.Create<GetCategoryByIdQueryValidator>();

    [Fact]
    public void Should_BeValid_When_CategoryIdIsValid()
    {
        // arrange
        var query = new GetCategoryByIdQuery
        {
            CategoryById = Guid.NewGuid()
        };

        // act & assert
        AssertValid(query);
    }

    [Fact]
    public void Should_BeNotValid_When_CategoryIdIsNotValid()
    {
        // arrange
        var query = new GetCategoryByIdQuery
        {
            CategoryById = Guid.Empty
        };

        // act & assert
        AssertNotValid(query);
    }
}
