using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketEventManagement.Application.DTOs;
using TicketEventManagement.Domain;

namespace TicketEventManagement.Application.Handlers.Events.Commands.UpdateEvent
{
    public class UpdateEventCommand : IRequest<GetEventDto>
    {
        public Guid EventId { get; set; }
        public string Name { get; set; } = default!;
        public string Description { get; set; } = default!;
        public string UriMainImage { get; set; } = default!;
        public DateTime DateTimeEventStart { get; set; }
        public DateTime DateTimeEventEnd { get; set; }
        public bool IsActive { get; set; }
    }
}
