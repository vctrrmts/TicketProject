using Core.Tests;
using Moq;
using TicketEventSearch.Application.Abstractions.Persistence;
using TicketEventSearch.Domain;
using Xunit.Abstractions;
using MediatR;
using Core.Tests.Attributes;
using TicketEventSearch.Application.Handlers.Schemes.Commands.UpdateScheme;
using System.Linq.Expressions;
using AutoFixture;

namespace SearchService.UnitTests.Tests.Schemes.Commands.UpdateScheme;

public class UpdateSchemeCommandHandlerTest : RequestHandlerTestBase<UpdateSchemeCommand>
{
    private readonly Mock<IBaseRepository<Scheme>> _schemesMock = new();

    public UpdateSchemeCommandHandlerTest(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
    {
    }

    protected override IRequestHandler<UpdateSchemeCommand> CommandHandler
        => new UpdateSchemeCommandHandler(_schemesMock.Object);

    [Theory, FixtureInlineAutoData]
    public async Task Should_ThrowNotFound_When_SchemeNotFound(UpdateSchemeCommand command)
    {
        // arrange
        _schemesMock.Setup(
            p => p.SingleOrDefaultAsync(It.IsAny<Expression<Func<Scheme, bool>>>(), default)
        ).ReturnsAsync(null as Scheme);

        // act and assert
        await AssertThrowNotFound(command);
    }

    [Theory, FixtureInlineAutoData]
    public async Task Should_BeValid_When_SchemeFound(UpdateSchemeCommand command)
    {
        // arrange
        var scheme = TestFixture.Build<Scheme>().Create();

        _schemesMock.Setup(
            p => p.SingleOrDefaultAsync(It.IsAny<Expression<Func<Scheme, bool>>>(), default)
        ).ReturnsAsync(scheme);

        scheme.UpdateName(command.Name);
        scheme.UpdateIsActive(command.IsActive);

        _schemesMock.Setup(
            p => p.UpdateAsync(scheme, default)).ReturnsAsync(scheme);

        // act and assert
        await AssertNotThrow(command);
    }
}
