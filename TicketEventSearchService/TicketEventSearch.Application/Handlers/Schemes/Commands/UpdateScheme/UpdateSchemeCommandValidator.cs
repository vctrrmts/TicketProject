using FluentValidation;

namespace TicketEventSearch.Application.Handlers.Schemes.Commands.UpdateScheme;

public class UpdateSchemeCommandValidator : AbstractValidator<UpdateSchemeCommand>
{
    public UpdateSchemeCommandValidator()
    {
        RuleFor(x => x.SchemeId).NotEmpty();
        RuleFor(x => x.Name).Length(1, 100).NotEmpty();
        RuleFor(x => x).NotEmpty();
    }
}
