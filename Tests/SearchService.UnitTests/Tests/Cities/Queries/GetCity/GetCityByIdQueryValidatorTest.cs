using AutoFixture;
using Core.Tests;
using FluentValidation;
using TicketEventSearch.Application.Handlers.Cities.Queries.GetCity;
using Xunit.Abstractions;

namespace SearchService.UnitTests.Tests.Cities.Queries.GetCity;

public class GetCityByIdQueryValidatorTest : ValidatorTestBase<GetCityByIdQuery>
{
    public GetCityByIdQueryValidatorTest(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
    {
    }

    protected override IValidator<GetCityByIdQuery> TestValidator 
        => TestFixture.Create<GetCityByIdQueryValidator>();

    [Fact]
    public void Should_BeValid_When_CityIdIsValid()
    {
        // arrange
        var query = new GetCityByIdQuery
        {
            CityId = Guid.NewGuid()
        };

        // act & assert
        AssertValid(query);
    }

    [Fact]
    public void Should_BeNotValid_When_CityIdIsNotValid()
    {
        // arrange
        var query = new GetCityByIdQuery
        {
            CityId = Guid.Empty
        };

        // act & assert
        AssertNotValid(query);
    }
}
