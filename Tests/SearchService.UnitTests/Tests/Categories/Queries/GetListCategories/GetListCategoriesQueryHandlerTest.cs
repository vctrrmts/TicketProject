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
using TicketEventSearch.Application.Abstractions.Caches.Category;
using TicketEventSearch.Application.Handlers.Categories.Queries.GetListCategories;
using TicketEventSearch.Application.Handlers.Cities.Queries.GetListCity;

namespace SearchService.UnitTests.Tests.Categories.Queries.GetListCategories;

public class GetListCategoriesQueryHandlerTest : RequestHandlerTestBase<GetListCategoriesQuery, IReadOnlyCollection<GetCategoryDto>>
{
    private readonly Mock<IBaseRepository<Category>> _categoriesMock = new();
    private readonly IMapper _mapper;
    private readonly Mock<ICategoryListCache> _cacheMock = new();

    public GetListCategoriesQueryHandlerTest(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
    {
        _mapper = new AutoMapperFixture(typeof(GetCategoryDto).Assembly).Mapper;
    }

    protected override IRequestHandler<GetListCategoriesQuery, IReadOnlyCollection<GetCategoryDto>> CommandHandler 
        => new GetListCategoriesQueryHandler(_categoriesMock.Object, _mapper, _cacheMock.Object);

    [Theory, FixtureInlineAutoData]
    public async Task Should_BeValid(GetListCategoriesQuery query)
    {
        // arrange
        var categories = TestFixture.Build<Category>().CreateMany(10).ToArray();

        _categoriesMock.Setup(
            p => p.GetListAsync(query.Offset, query.Limit, It.IsAny<Expression<Func<Category, bool>>>(), 
            default,default,default)).ReturnsAsync(categories);

        // act and assert
        await AssertNotThrow(query);
    }
}
