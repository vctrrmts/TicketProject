namespace TicketEventManagement.Application.DTOs;

public class GetEventDto
{
    public Guid EventId { get; set; } = default!;
    public string Name { get; set; } = default!;
    public string Description { get; set; } = default!;
    public string UriMainImage { get; set; } = default!;
    public DateTime DateTimeEventStart { get; set; }
    public DateTime DateTimeEventEnd { get; set; }
    public bool IsActive { get; set; }
    public bool IsPublish { get; set; }
    public GetCategoryForEventDto Category { get; set; } = default!;
    public GetLocationForEventDto Location { get; set; } = default!;
}
