namespace TicketEventManagement.Application.DTOs;

public class UpdateLocationCommandDto
{
    public string Name { get; set; } = default!;
    public string Address { get; set; } = default!;
    public double Latitude { get; set; }
    public double Longitude { get; set; }
    public bool IsActive { get; set; }
}
