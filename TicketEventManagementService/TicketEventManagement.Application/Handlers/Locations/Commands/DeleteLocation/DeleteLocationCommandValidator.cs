using FluentValidation;

namespace TicketEventManagement.Application.Handlers.Locations.Commands.DeleteLocation;

public class DeleteLocationCommandValidator : AbstractValidator<DeleteLocationCommand>
{
    public DeleteLocationCommandValidator()
    {
        RuleFor(x => x.LocationId).NotEmpty();
    }
}
