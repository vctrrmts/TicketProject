namespace TicketEventSearch.Application.DTOs;

public class GetCategoryDto
{
    public Guid CategoryId { get; set; }

    public string Name { get; set; } = default!;

}
