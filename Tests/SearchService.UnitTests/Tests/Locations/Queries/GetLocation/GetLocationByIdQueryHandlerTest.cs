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
using TicketEventSearch.Application.Handlers.Locations.Queries.GetLocation;
using TicketEventSearch.Application.Abstractions.Caches.Location;

namespace SearchService.UnitTests.Tests.Locations.Queries.GetLocation
{
    public class GetLocationByIdQueryHandlerTest : RequestHandlerTestBase<GetLocationByIdQuery, GetLocationDto>
    {
        private readonly Mock<IBaseRepository<Location>> _locationsMock = new();
        private readonly IMapper _mapper;
        private readonly Mock<ILocationCache> _cacheMock = new();

        public GetLocationByIdQueryHandlerTest(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
        {
            _mapper = new AutoMapperFixture(typeof(GetLocationDto).Assembly).Mapper;
        }

        protected override IRequestHandler<GetLocationByIdQuery, GetLocationDto> CommandHandler 
            => new GetLocationByIdQueryHandler(_locationsMock.Object, _mapper, _cacheMock.Object);

        [Fact]
        public async Task Should_BeValid_When_LocationFound()
        {
            // arrange
            var location = new Location(Guid.NewGuid(), Guid.NewGuid(), "Name", "Address", 0, 0, true);
            var query = new GetLocationByIdQuery() { LocationId = location.LocationId };
            _locationsMock.Setup(
                p => p.SingleOrDefaultAsync(It.IsAny<Expression<Func<Location, bool>>>(), default)
            ).ReturnsAsync(location);

            // act and assert
            await AssertNotThrow(query);
        }

        [Theory, FixtureInlineAutoData]
        public async Task Should_ThrowNotFound_When_LocationNotFound(GetLocationByIdQuery query)
        {
            // arrange
            _locationsMock.Setup(
                p => p.SingleOrDefaultAsync(It.IsAny<Expression<Func<Location, bool>>>(), default)
            ).ReturnsAsync(null as Location);

            // act and assert
            await AssertThrowNotFound(query);
        }
    }
}
