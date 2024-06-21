using AutoFixture;
using Core.Tests;
using Core.Tests.Attributes;
using FluentValidation;
using TicketEventSearch.Application.Handlers.Schemes.Commands.CreateScheme;
using Xunit.Abstractions;

namespace SearchService.UnitTests.Tests.Schemes.Commands.CreateScheme
{
    public class CreateSchemeCommandValidatorTest : ValidatorTestBase<CreateSchemeCommand>
    {
        public CreateSchemeCommandValidatorTest(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
        {
        }

        protected override IValidator<CreateSchemeCommand> TestValidator 
            => TestFixture.Create<CreateSchemeCommandValidator>();

        [Fact]
        public void Should_BeValid_When_RequestIsValid()
        {
            // arrange
            var command = new CreateSchemeCommand
            {
                SchemeId = Guid.NewGuid(),
                LocationId = Guid.NewGuid(),
                Name = "test"
            };

            // act & assert
            AssertValid(command);
        }

        [Fact]
        public void Should_BeNotValid_When_SchemeIdIsEmpty()
        {
            // arrange
            var command = new CreateSchemeCommand
            {
                SchemeId = Guid.Empty,
                LocationId = Guid.NewGuid(),
                Name = "test"
            };

            // act & assert
            AssertNotValid(command);
        }

        [Fact]
        public void Should_BeNotValid_When_LocationIdIsEmpty()
        {
            // arrange
            var command = new CreateSchemeCommand
            {
                SchemeId = Guid.NewGuid(),
                LocationId = Guid.Empty,
                Name = "test"
            };

            // act & assert
            AssertNotValid(command);
        }

        [Theory]
        [FixtureInlineAutoData("1")]
        [FixtureInlineAutoData("1234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890")]
        public void Should_BeValid_When_NameIsValid(string name)
        {
            // arrange
            var command = new CreateSchemeCommand
            {
                SchemeId = Guid.NewGuid(),
                LocationId = Guid.NewGuid(),
                Name = name
            };

            // act & assert
            AssertValid(command);
        }

        [Theory]
        [FixtureInlineAutoData("")]
        [FixtureInlineAutoData("   ")]
        [FixtureInlineAutoData(null)]
        [FixtureInlineAutoData("12345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901")]
        public void Should_NotBeValid_When_NameIsNotValid(string name)
        {
            // arrange
            var command = new CreateSchemeCommand
            {
                SchemeId = Guid.NewGuid(),
                LocationId = Guid.NewGuid(),
                Name = name
            };

            // act & assert
            AssertNotValid(command);
        }
    }
}
