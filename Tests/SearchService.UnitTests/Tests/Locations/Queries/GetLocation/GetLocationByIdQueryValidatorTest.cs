using AutoFixture;
using Core.Tests;
using FluentValidation;
using TicketEventSearch.Application.Handlers.Cities.Queries.GetCity;
using TicketEventSearch.Application.Handlers.Locations.Queries.GetLocation;
using Xunit.Abstractions;

namespace SearchService.UnitTests.Tests.Locations.Queries.GetLocation;

public class GetLocationByIdQueryValidatorTest : ValidatorTestBase<GetLocationByIdQuery>
{
    public GetLocationByIdQueryValidatorTest(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
    {
    }

    protected override IValidator<GetLocationByIdQuery> TestValidator 
        => TestFixture.Create<GetLocationByIdQueryValidator>();

    [Fact]
    public void Should_BeValid_When_LocationIdIsValid()
    {
        // arrange
        var query = new GetLocationByIdQuery
        {
            LocationId = Guid.NewGuid()
        };

        // act & assert
        AssertValid(query);
    }

    [Fact]
    public void Should_BeNotValid_When_LocationIdIsNotValid()
    {
        // arrange
        var query = new GetLocationByIdQuery
        {
            LocationId = Guid.Empty
        };

        // act & assert
        AssertNotValid(query);
    }
}
