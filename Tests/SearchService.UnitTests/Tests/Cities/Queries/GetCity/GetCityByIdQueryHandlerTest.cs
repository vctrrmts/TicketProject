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
using AutoFixture;
using TicketEventSearch.Application.Handlers.Cities.Queries.GetCity;
using TicketEventSearch.Application.Abstractions.Caches.City;

namespace SearchService.UnitTests.Tests.Cities.Queries.GetCity;

public class GetCityByIdQueryHandlerTest : RequestHandlerTestBase<GetCityByIdQuery, GetCityDto>
{
    private readonly Mock<IBaseRepository<City>> _citiesMock = new();
    private readonly IMapper _mapper;
    private readonly Mock<ICityCache> _cacheMock = new();
    public GetCityByIdQueryHandlerTest(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
    {
        _mapper = new AutoMapperFixture(typeof(GetCityDto).Assembly).Mapper;
    }

    protected override IRequestHandler<GetCityByIdQuery, GetCityDto> CommandHandler 
        => new GetCityByIdQueryHandler(_citiesMock.Object, _mapper, _cacheMock.Object);

    [Fact]
    public async Task Should_BeValid_When_CityyFound()
    {
        // arrange
        var city = TestFixture.Build<City>().Create();
        var query = new GetCityByIdQuery() { CityId = city.CityId };

        _citiesMock.Setup(
            p => p.SingleOrDefaultAsync(It.IsAny<Expression<Func<City, bool>>>(), default)
        ).ReturnsAsync(city);

        // act and assert
        await AssertNotThrow(query);
    }

    [Theory, FixtureInlineAutoData]
    public async Task Should_ThrowNotFound_When_CityNotFound(GetCityByIdQuery query)
    {
        // arrange
        _citiesMock.Setup(
            p => p.SingleOrDefaultAsync(It.IsAny<Expression<Func<City, bool>>>(), default)
        ).ReturnsAsync(null as City);

        // act and assert
        await AssertThrowNotFound(query);
    }
}
