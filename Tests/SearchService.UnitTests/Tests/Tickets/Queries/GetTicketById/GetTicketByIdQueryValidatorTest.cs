using AutoFixture;
using Core.Tests;
using FluentValidation;
using TicketEventSearch.Application.Handlers.Tickets.Queries.GetTicketById;
using Xunit.Abstractions;

namespace SearchService.UnitTests.Tests.Tickets.Queries.GetTicketById;

public class GetTicketByIdQueryValidatorTest : ValidatorTestBase<GetTicketByIdQuery>
{
    public GetTicketByIdQueryValidatorTest(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
    {
    }

    protected override IValidator<GetTicketByIdQuery> TestValidator 
        => TestFixture.Create<GetTicketByIdQueryValidator>();

    [Fact]
    public void Should_BeValid_When_TicketIdIsValid()
    {
        // arrange
        var query = new GetTicketByIdQuery
        {
            TicketId = Guid.NewGuid()
        };

        // act & assert
        AssertValid(query);
    }

    [Fact]
    public void Should_BeNotValid_When_TicketIdIsNotValid()
    {
        // arrange
        var query = new GetTicketByIdQuery
        {
            TicketId = Guid.Empty
        };

        // act & assert
        AssertNotValid(query);
    }
}
