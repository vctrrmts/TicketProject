namespace TicketEventManagement.Application.DTOs;

public class GetSchemeDto
{
    public Guid SchemeId { get; set; }
    public Guid LocationId { get; set; }
    public string Name { get; set; } = default!;
    public bool IsActive { get; set; }
}
