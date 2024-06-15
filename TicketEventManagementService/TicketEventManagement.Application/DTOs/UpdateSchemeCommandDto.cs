namespace TicketEventManagement.Application.DTOs;

public class UpdateSchemeCommandDto
{
    public string Name { get; set; } = default!;
    public bool IsActive { get; set; }
}
