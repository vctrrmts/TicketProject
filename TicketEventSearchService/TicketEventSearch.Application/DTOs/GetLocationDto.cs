namespace TicketEventSearch.Application.DTOs;

public class GetLocationDto
{
    public Guid LocationId { get; set; }
    public Guid CityId { get; set; }
    public string Name { get; set; }
    public string Address { get; set; }
    public double Latitude { get; set; }
    public double Longitude { get; set; }
}
