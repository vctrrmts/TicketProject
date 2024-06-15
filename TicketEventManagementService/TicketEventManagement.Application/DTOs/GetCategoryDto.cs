namespace TicketEventManagement.Application.DTOs;

public class GetCategoryDto
{
    public Guid CategoryId { get; set; }

    public string Name { get; set; } = default!;

    public bool IsActive { get; set; }
}
