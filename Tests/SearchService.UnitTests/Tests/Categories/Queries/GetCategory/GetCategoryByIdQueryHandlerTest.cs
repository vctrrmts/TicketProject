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
using TicketEventSearch.Application.Handlers.Categories.Queries.GetCategory;
using TicketEventSearch.Application.Abstractions.Caches.Category;

namespace SearchService.UnitTests.Tests.Categories.Queries.GetCategory;

public class GetCategoryByIdQueryHandlerTest : RequestHandlerTestBase<GetCategoryByIdQuery, GetCategoryDto>
{
    private readonly Mock<IBaseRepository<Category>> _categoriesMock = new();
    private readonly IMapper _mapper;
    private readonly Mock<ICategoryCache> _cacheMock = new();

    public GetCategoryByIdQueryHandlerTest(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
    {
        _mapper = new AutoMapperFixture(typeof(GetCategoryDto).Assembly).Mapper;
    }

    protected override IRequestHandler<GetCategoryByIdQuery, GetCategoryDto> CommandHandler 
        => new GetCategoryByIdQueryHandler(_categoriesMock.Object, _mapper, _cacheMock.Object);

    [Fact]
    public async Task Should_BeValid_When_CategoryFound()
    {
        // arrange
        var category = TestFixture.Build<Category>().Create();
        var query = new GetCategoryByIdQuery() {CategoryById = category.CategoryId };

        _categoriesMock.Setup(
            p => p.SingleOrDefaultAsync(It.IsAny<Expression<Func<Category, bool>>>(), default)
        ).ReturnsAsync(category);

        // act and assert
        await AssertNotThrow(query);
    }

    [Theory, FixtureInlineAutoData]
    public async Task Should_ThrowNotFound_When_CategoryNotFound(GetCategoryByIdQuery query)
    {
        // arrange
        _categoriesMock.Setup(
            p => p.SingleOrDefaultAsync(It.IsAny<Expression<Func<Category, bool>>>(), default)
        ).ReturnsAsync(null as Category);

        // act and assert
        await AssertThrowNotFound(query);
    }
}
