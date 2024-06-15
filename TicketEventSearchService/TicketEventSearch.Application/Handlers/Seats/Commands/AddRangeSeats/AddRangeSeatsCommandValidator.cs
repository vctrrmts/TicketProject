using FluentValidation;

namespace TicketEventSearch.Application.Handlers.Seats.Commands.AddRangeSeats;

public class AddRangeSeatsCommandValidator : AbstractValidator<AddRangeSeatsCommand>
{
    public AddRangeSeatsCommandValidator() 
    {
        RuleFor(x => x.Seats).NotEmpty();
    }
}
