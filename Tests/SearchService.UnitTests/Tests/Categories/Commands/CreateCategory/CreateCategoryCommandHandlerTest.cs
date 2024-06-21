using Core.Tests;
using MediatR;
using Moq;
using TicketEventSearch.Application.DTOs;
using TicketEventSearch.Application.Abstractions.Persistence;
using TicketEventSearch.Application.Caches.Category;
using TicketEventSearch.Domain;
using AutoMapper;
using Core.Tests.Fixtures;
using Xunit.Abstractions;
using TicketEventSearch.Application.Handlers.Categories.Commands.CreateCategory;
using Core.Tests.Attributes;

namespace SearchService.UnitTests.Tests.Categories.Commands.CreateCategory;

public class CreateCategoryCommandHandlerTest : RequestHandlerTestBase<CreateCategoryCommand, GetCategoryDto>
{
    private readonly Mock<IBaseRepository<Category>> _categoriesMock = new();
    private readonly IMapper _mapper;
    private readonly Mock<ICleanCategoryCacheService> _cleanCategoryCacheService = new();

    public CreateCategoryCommandHandlerTest(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
    {
        _mapper = new AutoMapperFixture(typeof(GetCategoryDto).Assembly).Mapper;
    }

    protected override IRequestHandler<CreateCategoryCommand, GetCategoryDto> CommandHandler 
        => new CreateCategoryCommandHandler(_categoriesMock.Object, _mapper, _cleanCategoryCacheService.Object);

    [Theory, FixtureInlineAutoData]
    public async Task Should_BeValid(CreateCategoryCommand command)
    {
        // arrange

        Category category = new Category(command.CategoryId,command.Name,command.IsActive);

        _categoriesMock.Setup(p => p.AddAsync(category, default)).ReturnsAsync(category);

        _cleanCategoryCacheService.Object.ClearAllCategoryCaches();

        // act and assert
        await AssertNotThrow(command);
    }
}
