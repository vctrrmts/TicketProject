using FluentValidation;

namespace TicketEventManagement.Application.Handlers.Tickets.Commands.DeleteTicket
{
    public class DeleteTicketsCommandValidator : AbstractValidator<DeleteTicketsCommand>
    {
        public DeleteTicketsCommandValidator()
        {
            RuleFor(x => x.EventId).NotEmpty();
        }
    }
}
