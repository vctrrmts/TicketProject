using Core.Tests;
using Moq;
using TicketEventSearch.Application.DTOs;
using TicketEventSearch.Application.Abstractions.Persistence;
using TicketEventSearch.Domain;
using AutoMapper;
using Core.Tests.Fixtures;
using Xunit.Abstractions;
using MediatR;
using TicketEventSearch.Application.Caches.City;
using Core.Tests.Attributes;
using TicketEventSearch.Application.Handlers.Cities.Commands.UpdateCity;
using AutoFixture;
using System.Linq.Expressions;

namespace SearchService.UnitTests.Tests.Cities.Commands.UpdateCity;

public class UpdateCityCommandHandlerTest : RequestHandlerTestBase<UpdateCityCommand, GetCityDto>
{
    private readonly Mock<IBaseRepository<City>> _citiesMock = new();
    private readonly IMapper _mapper;
    private readonly Mock<ICleanCityCacheService> _cleanCityCacheService = new();

    public UpdateCityCommandHandlerTest(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
    {
        _mapper = new AutoMapperFixture(typeof(GetCityDto).Assembly).Mapper;
    }

    protected override IRequestHandler<UpdateCityCommand, GetCityDto> CommandHandler 
        => new UpdateCityCommandHandler(_citiesMock.Object, _mapper, _cleanCityCacheService.Object);

    [Theory, FixtureInlineAutoData]
    public async Task Should_BeValid_When_CityFound(UpdateCityCommand command)
    {
        // arrange
        var city = TestFixture.Build<City>().Create();

        _citiesMock.Setup(
            p => p.SingleOrDefaultAsync(It.IsAny<Expression<Func<City, bool>>>(), default)
        ).ReturnsAsync(city);

        city.UpdateName(command.Name);
        city.UpdateIsActive(command.IsActive);

        _citiesMock.Setup(
            p => p.UpdateAsync(city, default)).ReturnsAsync
            (city);

        _cleanCityCacheService.Object.ClearAllCityCaches();

        // act and assert
        await AssertNotThrow(command);
    }

    [Theory, FixtureInlineAutoData]
    public async Task Should_ThrowNotFound_When_CityNotFound(UpdateCityCommand command)
    {
        // arrange
        _citiesMock.Setup(
            p => p.SingleOrDefaultAsync(It.IsAny<Expression<Func<City, bool>>>(), default)
        ).ReturnsAsync(null as City);

        // act and assert
        await AssertThrowNotFound(command);
    }
}

