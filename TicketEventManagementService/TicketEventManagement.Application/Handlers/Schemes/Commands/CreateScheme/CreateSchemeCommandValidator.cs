using FluentValidation;

namespace TicketEventManagement.Application.Handlers.Schemes.Commands.CreateScheme;

public class CreateSchemeCommandValidator : AbstractValidator<CreateSchemeCommand>
{
    public CreateSchemeCommandValidator()
    {
        RuleFor(x => x.LocationId).NotEmpty();
        RuleFor(x => x.Name).Length(1, 100).NotEmpty();
    }
}
