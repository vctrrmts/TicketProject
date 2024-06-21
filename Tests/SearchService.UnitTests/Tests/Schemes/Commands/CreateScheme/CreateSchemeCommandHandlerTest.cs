using Core.Tests;
using Moq;
using TicketEventSearch.Application.Abstractions.Persistence;
using TicketEventSearch.Domain;
using Xunit.Abstractions;
using MediatR;
using Core.Tests.Attributes;
using TicketEventSearch.Application.Handlers.Schemes.Commands.CreateScheme;

namespace SearchService.UnitTests.Tests.Schemes.Commands.CreateScheme;

public class CreateSchemeCommandHandlerTest : RequestHandlerTestBase<CreateSchemeCommand>
{
    private readonly Mock<IBaseRepository<Scheme>> _schemesMock = new();

    public CreateSchemeCommandHandlerTest(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
    {
    }

    protected override IRequestHandler<CreateSchemeCommand> CommandHandler
        => new CreateSchemeCommandHandler(_schemesMock.Object);

    [Theory, FixtureInlineAutoData]
    public async Task Should_BeValid(CreateSchemeCommand command)
    {
        // arrange
        var scheme = new Scheme(command.SchemeId, command.LocationId, command.Name);
        _schemesMock.Setup(p => p.AddAsync(scheme, default)).ReturnsAsync(scheme);

        // act and assert
        await AssertNotThrow(command);
    }
}
