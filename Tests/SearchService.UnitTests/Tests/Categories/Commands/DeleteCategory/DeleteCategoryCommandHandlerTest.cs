using Core.Tests;
using System.Linq.Expressions;
using Moq;
using TicketEventSearch.Application.Abstractions.Persistence;
using TicketEventSearch.Application.Caches.Category;
using TicketEventSearch.Domain;
using Xunit.Abstractions;
using MediatR;
using Core.Tests.Attributes;
using AutoFixture;
using TicketEventSearch.Application.Handlers.Categories.Commands.DeleteCategory;

namespace SearchService.UnitTests.Tests.Categories.Commands.DeleteCategory;

public class DeleteCategoryCommandHandlerTest : RequestHandlerTestBase<DeleteCategoryCommand>
{
    private readonly Mock<IBaseRepository<Category>> _categoriesMock = new();
    private readonly Mock<ICleanCategoryCacheService> _cleanCategoryCacheService = new();

    public DeleteCategoryCommandHandlerTest(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
    {
    }

    protected override IRequestHandler<DeleteCategoryCommand> CommandHandler 
        => new DeleteCategoryCommandHandler(_categoriesMock.Object, _cleanCategoryCacheService.Object);


    [Theory, FixtureInlineAutoData]
    public async Task Should_ThrowNotFound_When_CategoryNotFound(DeleteCategoryCommand command)
    {
        // arrange
        _categoriesMock.Setup(
            p => p.SingleOrDefaultAsync(It.IsAny<Expression<Func<Category, bool>>>(), default)
        ).ReturnsAsync(null as Category);

        // act and assert
        await AssertThrowNotFound(command);
    }

    [Theory, FixtureInlineAutoData]
    public async Task Should_BeValid_When_CategoryFound(DeleteCategoryCommand command)
    {
        // arrange
        var category = TestFixture.Build<Category>().Create();

        _categoriesMock.Setup(
            p => p.SingleOrDefaultAsync(It.IsAny<Expression<Func<Category, bool>>>(), default)
        ).ReturnsAsync(category);

        category.UpdateIsActive(false);

        _categoriesMock.Setup(
            p => p.UpdateAsync(category, default)).ReturnsAsync
            (category);

        _cleanCategoryCacheService.Object.ClearAllCategoryCaches();

        // act and assert
        await AssertNotThrow(command);
    }
}
