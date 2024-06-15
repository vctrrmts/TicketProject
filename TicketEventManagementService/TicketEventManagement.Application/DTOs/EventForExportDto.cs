namespace TicketEventManagement.Application.DTOs;

public class EventForExportDto
{
    public Guid EventId { get; set; }
    public Guid LocationId { get; set; }
    public Guid CategoryId { get; set; }
    public string Name { get; set; } = default!;
    public string Description { get; set; } = default!;
    public string UriMainImage { get; set; } = default!;
    public DateTime DateTimeEventStart { get; set; }
    public DateTime DateTimeEventEnd { get; set; }
    public bool IsActive { get; set; }
    public ICollection<TicketForExportDto> Tickets { get; set; } = default!;
}
