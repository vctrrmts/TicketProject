using FluentValidation;

namespace TicketEventManagement.Application.Handlers.Schemes.Commands.DeleteScheme;

public class DeleteSchemeCommandValidator : AbstractValidator<DeleteSchemeCommand>
{
    public DeleteSchemeCommandValidator()
    {
        RuleFor(e => e.SchemeId).NotEmpty();
    }
}
