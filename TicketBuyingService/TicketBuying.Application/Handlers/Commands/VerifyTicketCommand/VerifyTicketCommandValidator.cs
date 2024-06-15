using FluentValidation;

namespace TicketBuying.Application.Handlers.Commands.VerifyTicketCommand
{
    public class VerifyTicketCommandValidator : AbstractValidator<VerifyTicketCommand>
    {
        public VerifyTicketCommandValidator()
        {
            RuleFor(x => x.HashGuid).NotEmpty();
        }
    }
}
