using Core.Tests;
using System.Linq.Expressions;
using Moq;
using TicketEventSearch.Application.DTOs;
using TicketEventSearch.Application.Abstractions.Persistence;
using TicketEventSearch.Domain;
using AutoMapper;
using Core.Tests.Fixtures;
using Xunit.Abstractions;
using MediatR;
using Core.Tests.Attributes;
using TicketEventSearch.Application.Abstractions.Caches.Location;
using TicketEventSearch.Application.Handlers.Locations.Queries.GetListLocations;

namespace SearchService.UnitTests.Tests.Locations.Queries.GetListLocations;

public class GetListLocationQueryHandlerTest : RequestHandlerTestBase<GetListLocationQuery, IReadOnlyCollection<GetLocationDto>>
{
    private readonly Mock<IBaseRepository<Location>> _locationsMock = new();
    private readonly IMapper _mapper;
    private readonly Mock<ILocationListCache> _cacheMock = new();

    public GetListLocationQueryHandlerTest(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
    {
        _mapper = new AutoMapperFixture(typeof(GetLocationDto).Assembly).Mapper;
    }

    protected override IRequestHandler<GetListLocationQuery, IReadOnlyCollection<GetLocationDto>> CommandHandler 
        => new GetListLocationQueryHandler(_locationsMock.Object, _mapper, _cacheMock.Object);

    [Theory, FixtureInlineAutoData]
    public async Task Should_BeValid(GetListLocationQuery query)
    {
        // arrange
        var locations = new Location[10];
        for (int i = 0; i < 10; i++)
        {
            locations[i] = new Location(Guid.NewGuid(), Guid.NewGuid(), "Name", "Address", 0, 0, true);
        }

        _locationsMock.Setup(
            p => p.GetListAsync(query.Offset, query.Limit, It.IsAny<Expression<Func<Location, bool>>>(),
            default, default, default)).ReturnsAsync(locations);

        // act and assert
        await AssertNotThrow(query);
    }
}
