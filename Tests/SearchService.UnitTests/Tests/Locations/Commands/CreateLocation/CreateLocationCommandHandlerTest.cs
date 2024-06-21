using Core.Tests;
using Moq;
using TicketEventSearch.Application.Abstractions.Persistence;
using TicketEventSearch.Domain;
using Xunit.Abstractions;
using MediatR;
using Core.Tests.Attributes;
using TicketEventSearch.Application.Handlers.Locations.Commands.CreateLocation;
using TicketEventSearch.Application.Caches.Location;

namespace SearchService.UnitTests.Tests.Locations.Commands.CreateLocation;

public class CreateLocationCommandHandlerTest : RequestHandlerTestBase<CreateLocationCommand>
{
    private readonly Mock<IBaseRepository<Location>> _locationsMock = new();
    private readonly Mock<ICleanLocationCacheService> _cleanLocationCacheService = new();

    public CreateLocationCommandHandlerTest(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
    {
    }

    protected override IRequestHandler<CreateLocationCommand> CommandHandler
        => new CreateLocationCommandHandler(_locationsMock.Object, _cleanLocationCacheService.Object);

    [Theory, FixtureInlineAutoData]
    public async Task Should_BeValid(CreateLocationCommand command)
    {
        // arrange
        command.Latitude = 0;
        command.Longitude = 0;
        var location = new Location(command.LocationId, command.CityId, command.Name,
        command.Address, command.Latitude, command.Longitude, command.IsActive);

        _locationsMock.Setup(p => p.AddAsync(location, default)).ReturnsAsync(location);

        _cleanLocationCacheService.Object.ClearListLocationCaches();
        // act and assert
        await AssertNotThrow(command);
    }
}
