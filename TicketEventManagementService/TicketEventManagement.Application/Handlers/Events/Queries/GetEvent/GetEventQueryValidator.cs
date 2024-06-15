using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketEventManagement.Application.Handlers.Events.Queries.GetEvent
{
    public class GetEventQueryValidator : AbstractValidator<GetEventQuery>
    {
        public GetEventQueryValidator() 
        {
            RuleFor(e => e.EventId).NotEmpty();
        }
    }
}
