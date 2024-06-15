namespace TicketEventManagement.Application.DTOs;

public class GetLocationForEventDto
{
    public string Name { get; set; }
    public string Address { get; set; }
    public double Latitude { get; set; }
    public double Longitude { get; set; }
}
