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
using TicketEventSearch.Application.Handlers.Locations.Commands.DeleteLocation;

namespace SearchService.UnitTests.Tests.Locations.Commands.DeleteLocation;

public class DeleteLocationCommandHandlerTest : RequestHandlerTestBase<DeleteLocationCommand>
{
    private readonly Mock<IBaseRepository<Location>> _locationsMock = new();
    private readonly Mock<ICleanLocationCacheService> _cleanLocationCacheService = new();
    public DeleteLocationCommandHandlerTest(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
    {
    }

    protected override IRequestHandler<DeleteLocationCommand> CommandHandler
        => new DeleteLocationCommandHandler(_locationsMock.Object, _cleanLocationCacheService.Object);

    [Theory, FixtureInlineAutoData]
    public async Task Should_ThrowNotFound_When_LocationNotFound(DeleteLocationCommand command)
    {
        // arrange
        _locationsMock.Setup(
            p => p.SingleOrDefaultAsync(It.IsAny<Expression<Func<Location, bool>>>(), default)
        ).ReturnsAsync(null as Location);

        // act and assert
        await AssertThrowNotFound(command);
    }

    [Theory, FixtureInlineAutoData]
    public async Task Should_BeValid_When_CategoryFound(DeleteLocationCommand command)
    {
        // arrange
        var location = new Location(command.LocationId, Guid.NewGuid(),
            "test", "test", 0, 0, true);

        _locationsMock.Setup(
            p => p.SingleOrDefaultAsync(It.IsAny<Expression<Func<Location, bool>>>(), default)
        ).ReturnsAsync(location);

        location.UpdateIsActive(false);

        _locationsMock.Setup(
            p => p.UpdateAsync(location, default)).ReturnsAsync
            (location);

        _cleanLocationCacheService.Object.ClearAllLocationCaches();

        // act and assert
        await AssertNotThrow(command);
    }
}
