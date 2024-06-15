using FluentValidation;

namespace TicketEventManagement.Application.Handlers.Seats.Commands.AddRangeSeats;

public class AddRangeSeatsCommandValidator : AbstractValidator<AddRangeSeatsCommand>
{
    public AddRangeSeatsCommandValidator() 
    {
        RuleFor(e => e.SchemeId).NotEmpty();

        RuleFor(e => e.RowNumberStart).GreaterThan(0).LessThanOrEqualTo(200).When(e => e.RowNumberStart.HasValue);
        RuleFor(e => e.RowNumberEnd).GreaterThan(0).LessThan(200).When(e => e.RowNumberEnd.HasValue);
        RuleFor(e => e.RowNumberStart).LessThanOrEqualTo(e => e.RowNumberEnd)
            .When(e => e.RowNumberStart.HasValue).When(e => e.RowNumberEnd.HasValue);

        RuleFor(e => e.SeatNumberStart).GreaterThan(0).LessThanOrEqualTo(2000);
        RuleFor(e => e.SeatNumberEnd).GreaterThan(0).LessThan(2000);
        RuleFor(e => e.SeatNumberStart).LessThanOrEqualTo(e => e.SeatNumberEnd);
    }
}
