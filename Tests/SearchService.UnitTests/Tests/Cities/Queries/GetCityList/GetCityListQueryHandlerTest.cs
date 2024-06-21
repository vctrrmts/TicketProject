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
using TicketEventSearch.Application.Abstractions.Caches.City;
using TicketEventSearch.Application.Handlers.Cities.Queries.GetListCity;

namespace SearchService.UnitTests.Tests.Cities.Queries.GetCityList;

public class GetCityListQueryHandlerTest : RequestHandlerTestBase<GetCityListQuery, IReadOnlyCollection<GetCityDto>>
{
    private readonly Mock<IBaseRepository<City>> _citiesMock = new();
    private readonly IMapper _mapper;
    private readonly Mock<ICityListCache> _cacheMock = new();

    public GetCityListQueryHandlerTest(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
    {
        _mapper = new AutoMapperFixture(typeof(GetCityDto).Assembly).Mapper;
    }

    protected override IRequestHandler<GetCityListQuery, IReadOnlyCollection<GetCityDto>> CommandHandler 
        => new GetCityListQueryHandler(_citiesMock.Object, _mapper, _cacheMock.Object);

    [Theory, FixtureInlineAutoData]
    public async Task Should_BeValid(GetCityListQuery query)
    {
        // arrange
        var cities = TestFixture.Build<City>().CreateMany(10).ToArray();

        _citiesMock.Setup(
            p => p.GetListAsync(query.Offset, query.Limit, It.IsAny<Expression<Func<City, bool>>>(),
            default, default, default)).ReturnsAsync(cities);

        // act and assert
        await AssertNotThrow(query);
    }
}
