using Core.Tests;
using Moq;
using TicketEventSearch.Application.DTOs;
using TicketEventSearch.Application.Abstractions.Persistence;
using TicketEventSearch.Domain;
using AutoMapper;
using Core.Tests.Fixtures;
using Xunit.Abstractions;
using MediatR;
using TicketEventSearch.Application.Handlers.Cities.Commands.CreateCity;
using TicketEventSearch.Application.Caches.City;
using Core.Tests.Attributes;

namespace SearchService.UnitTests.Tests.Cities.Commands.CreateCity;

public class CreateCityCommandHandlerTest : RequestHandlerTestBase<CreateCityCommand, GetCityDto>
{
    private readonly Mock<IBaseRepository<City>> _citiesMock = new();
    private readonly IMapper _mapper;
    private readonly Mock<ICleanCityCacheService> _cleanCityCacheService = new();

    public CreateCityCommandHandlerTest(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
    {
        _mapper = new AutoMapperFixture(typeof(GetCityDto).Assembly).Mapper;
    }

    protected override IRequestHandler<CreateCityCommand, GetCityDto> CommandHandler 
        => new CreateCityCommandHandler(_citiesMock.Object, _mapper, _cleanCityCacheService.Object);

    [Theory, FixtureInlineAutoData]
    public async Task Should_BeValid(CreateCityCommand command)
    {
        // arrange

        var city = new City(command.CityId, command.Name, command.IsActive);

        _citiesMock.Setup(p => p.AddAsync(city, default)).ReturnsAsync(city);

        _cleanCityCacheService.Object.ClearAllCityCaches();

        // act and assert
        await AssertNotThrow(command);
    }
}
