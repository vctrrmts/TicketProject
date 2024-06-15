namespace TicketEventManagement.Application.DTOs;

public class UpdateEventCommandDto
{
    public string Name { get; set; } = default!;
    public string Description { get; set; } = default!;
    public string UriMainImage { get; set; } = default!;
    public DateTime DateTimeEventStart { get; set; }
    public DateTime DateTimeEventEnd { get; set; }
    public bool IsActive { get; set; }
}
