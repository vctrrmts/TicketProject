using Core.Tests;
using System.Linq.Expressions;
using Moq;
using TicketEventSearch.Application.DTOs;
using TicketEventSearch.Application.Handlers.Categories.Commands.UpdateCategory;
using TicketEventSearch.Application.Abstractions.Persistence;
using TicketEventSearch.Application.Caches.Category;
using TicketEventSearch.Domain;
using AutoMapper;
using Core.Tests.Fixtures;
using Xunit.Abstractions;
using MediatR;
using Core.Tests.Attributes;
using AutoFixture;

namespace SearchService.UnitTests.Tests.Categories.Commands.UpdateCategory;

public class UpdateCategoryCommandHandlerTest : RequestHandlerTestBase<UpdateCategoryCommand, GetCategoryDto>
{
    private readonly Mock<IBaseRepository<Category>> _categoriesMock = new();
    private readonly IMapper _mapper;
    private readonly Mock<ICleanCategoryCacheService> _cleanCategoryCacheService = new();

    public UpdateCategoryCommandHandlerTest(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
    {
        _mapper = new AutoMapperFixture(typeof(GetCategoryDto).Assembly).Mapper;
    }

    protected override IRequestHandler<UpdateCategoryCommand, GetCategoryDto> CommandHandler =>
        new UpdateCategoryCommandHandler(_categoriesMock.Object, _mapper, _cleanCategoryCacheService.Object);

    [Theory, FixtureInlineAutoData]
    public async Task Should_ThrowNotFound_When_CategoryNotFound(UpdateCategoryCommand command)
    {
        // arrange
        _categoriesMock.Setup(
            p => p.SingleOrDefaultAsync(It.IsAny<Expression<Func<Category, bool>>>(), default)
        ).ReturnsAsync(null as Category);

        // act and assert
        await AssertThrowNotFound(command);
    }

    [Theory, FixtureInlineAutoData]
    public async Task Should_BeValid_When_CategoryFound(UpdateCategoryCommand command)
    {
        // arrange
        var category = TestFixture.Build<Category>().Create();

        _categoriesMock.Setup(
            p => p.SingleOrDefaultAsync(It.IsAny<Expression<Func<Category, bool>>>(), default)
        ).ReturnsAsync(category);

        category.UpdateName(command.Name);
        category.UpdateIsActive(command.IsActive);

        _categoriesMock.Setup(p=>p.UpdateAsync(category,default)).ReturnsAsync(category);

        _cleanCategoryCacheService.Object.ClearAllCategoryCaches();

        // act and assert
        await AssertNotThrow(command);
    }
}
