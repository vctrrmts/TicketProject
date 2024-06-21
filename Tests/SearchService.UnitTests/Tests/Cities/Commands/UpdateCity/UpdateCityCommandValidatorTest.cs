﻿using AutoFixture;
using Core.Tests;
using Core.Tests.Attributes;
using FluentValidation;
using TicketEventSearch.Application.Handlers.Cities.Commands.UpdateCity;
using Xunit.Abstractions;

namespace SearchService.UnitTests.Tests.Cities.Commands.UpdateCity;

public class UpdateCityCommandValidatorTest : ValidatorTestBase<UpdateCityCommand>
{
    public UpdateCityCommandValidatorTest(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
    {
    }

    protected override IValidator<UpdateCityCommand> TestValidator 
        => TestFixture.Create<UpdateCityCommandValidator>();

    [Fact]
    public void Should_BeValid_When_RequestIsValid()
    {
        // arrange
        var command = new UpdateCityCommand
        {
            CityId = Guid.NewGuid(),
            Name = "test",
            IsActive = true
        };

        // act & assert
        AssertValid(command);
    }

    [Theory]
    [FixtureInlineAutoData("1")]
    [FixtureInlineAutoData("12345678901234567890123456789012345678901234567890")]
    public void Should_BeValid_When_NameIsValid(string name)
    {
        // arrange
        var command = new UpdateCityCommand
        {
            CityId = Guid.NewGuid(),
            Name = name,
        };

        // act & assert
        AssertValid(command);
    }

    [Theory]
    [FixtureInlineAutoData("")]
    [FixtureInlineAutoData("   ")]
    [FixtureInlineAutoData(null)]
    [FixtureInlineAutoData("123456789012345678901234567890123456789012345678901")]
    public void Should_NotBeValid_When_NameIsNotValid(string name)
    {
        // arrange
        var command = new UpdateCityCommand
        {
            CityId = Guid.NewGuid(),
            Name = name,
        };

        // act & assert
        AssertNotValid(command);
    }

    [Fact]
    public void Should_BeValid_When_CityIdIsValid()
    {
        // arrange
        var command = new UpdateCityCommand
        {
            CityId = Guid.NewGuid(),
            Name = "test",
        };

        // act & assert
        AssertValid(command);
    }

    [Fact]
    public void Should_BeNotValid_When_CityIdIsNotValid()
    {
        // arrange
        var command = new UpdateCityCommand
        {
            CityId = Guid.Empty,
            Name = "test",
        };

        // act & assert
        AssertNotValid(command);
    }
}
