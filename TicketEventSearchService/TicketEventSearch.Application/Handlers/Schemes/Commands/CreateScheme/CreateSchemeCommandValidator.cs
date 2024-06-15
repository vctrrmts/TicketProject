﻿using FluentValidation;

namespace TicketEventSearch.Application.Handlers.Schemes.Commands.CreateScheme;

public class CreateSchemeCommandValidator : AbstractValidator<CreateSchemeCommand>
{
    public CreateSchemeCommandValidator()
    {
        RuleFor(x => x.SchemeId).NotEmpty();
        RuleFor(x => x.LocationId).NotEmpty();
        RuleFor(x => x.Name).Length(1, 100).NotEmpty();
    }
}
