using Core.Tests;
using Moq;
using TicketEventSearch.Application.Abstractions.Persistence;
using TicketEventSearch.Domain;
using Xunit.Abstractions;
using MediatR;
using TicketEventSearch.Application.Caches.City;
using Core.Tests.Attributes;
using TicketEventSearch.Application.Handlers.Cities.Commands.DeleteCity;
using System.Linq.Expressions;
using AutoFixture;

namespace SearchService.UnitTests.Tests.Cities.Commands.DeleteCity;

public class DeleteCityCommandHandlerTest : RequestHandlerTestBase<DeleteCityCommand>
{
    private readonly Mock<IBaseRepository<City>> _citiesMock = new();
    private readonly Mock<ICleanCityCacheService> _cleanCityCacheService = new();

    public DeleteCityCommandHandlerTest(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
    {
    }

    protected override IRequestHandler<DeleteCityCommand> CommandHandler 
        => new DeleteCityCommandHandler(_citiesMock.Object,_cleanCityCacheService.Object);

    [Theory, FixtureInlineAutoData]
    public async Task Should_BeValid_When_CityFound(DeleteCityCommand command)
    {
        // arrange
        var city = TestFixture.Build<City>().Create();

        _citiesMock.Setup(
            p => p.SingleOrDefaultAsync(It.IsAny<Expression<Func<City, bool>>>(), default)
        ).ReturnsAsync(city);

        city.UpdateIsActive(false);

        _citiesMock.Setup(
            p => p.UpdateAsync(city, default)).ReturnsAsync
            (city);

        _cleanCityCacheService.Object.ClearAllCityCaches();

        // act and assert
        await AssertNotThrow(command);
    }

    [Theory, FixtureInlineAutoData]
    public async Task Should_ThrowNotFound_When_CityNotFound(DeleteCityCommand command)
    {
        // arrange
        _citiesMock.Setup(
            p => p.SingleOrDefaultAsync(It.IsAny<Expression<Func<City, bool>>>(), default)
        ).ReturnsAsync(null as City);

        // act and assert
        await AssertThrowNotFound(command);
    }


}
