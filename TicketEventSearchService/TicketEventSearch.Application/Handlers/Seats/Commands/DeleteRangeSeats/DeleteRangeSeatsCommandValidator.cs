using FluentValidation;

namespace TicketEventSearch.Application.Handlers.Seats.Commands.DeleteRangeSeats;

public class DeleteRangeSeatsCommandValidator : AbstractValidator<DeleteRangeSeatsCommand>
{
    public DeleteRangeSeatsCommandValidator()
    {
        RuleFor(x=>x.Seats).NotEmpty();
    }
}
