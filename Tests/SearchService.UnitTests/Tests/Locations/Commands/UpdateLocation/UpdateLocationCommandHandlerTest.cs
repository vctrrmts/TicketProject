using Core.Tests;
using Moq;
using TicketEventSearch.Application.Abstractions.Persistence;
using TicketEventSearch.Domain;
using Xunit.Abstractions;
using MediatR;
using Core.Tests.Attributes;
using TicketEventSearch.Application.Caches.Location;
using TicketEventSearch.Application.Handlers.Locations.Commands.UpdateLocation;
using System.Linq.Expressions;

namespace SearchService.UnitTests.Tests.Locations.Commands.UpdateLocation;

public class UpdateLocationCommandHandlerTest : RequestHandlerTestBase<UpdateLocationCommand>
{
    private readonly Mock<IBaseRepository<Location>> _locationsMock = new();
    private readonly Mock<ICleanLocationCacheService> _cleanLocationCacheService = new();

    public UpdateLocationCommandHandlerTest(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
    {
    }

    protected override IRequestHandler<UpdateLocationCommand> CommandHandler
        => new UpdateLocationCommandHandler(_locationsMock.Object, _cleanLocationCacheService.Object);

    [Theory, FixtureInlineAutoData]
    public async Task Should_ThrowNotFound_When_LocationNotFound(UpdateLocationCommand command)
    {
        // arrange
        _locationsMock.Setup(
            p => p.SingleOrDefaultAsync(It.IsAny<Expression<Func<Location, bool>>>(), default)
        ).ReturnsAsync(null as Location);

        // act and assert
        await AssertThrowNotFound(command);
    }

    [Theory, FixtureInlineAutoData]
    public async Task Should_BeValid_When_CategoryFound(UpdateLocationCommand command)
    {
        // arrange
        command.Latitude = 0;
        command.Longitude = 0;
        //var location = TestFixture.Build<Location>().Create();
        var location = new Location(command.LocationId, Guid.NewGuid(),
            command.Name, command.Address, command.Latitude, command.Longitude, command.IsActive);

        _locationsMock.Setup(
            p => p.SingleOrDefaultAsync(It.IsAny<Expression<Func<Location, bool>>>(), default)
        ).ReturnsAsync(location);

        location.UpdateName(command.Name);
        location.UpdateAddress(command.Address);
        location.UpdateLatitude(command.Latitude);
        location.UpdateLongitude(command.Longitude);
        location.UpdateIsActive(command.IsActive);

        _locationsMock.Setup(
            p => p.UpdateAsync(location, default)).ReturnsAsync
            (location);

        _cleanLocationCacheService.Object.ClearAllLocationCaches();

        // act and assert
        await AssertNotThrow(command);
    }
}
