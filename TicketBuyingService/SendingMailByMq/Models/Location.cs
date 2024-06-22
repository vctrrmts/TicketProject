namespace SendingMailByMq.Models;

public class Location
{
    public string Name { get; set; } = default!;
    public string Address { get; set; } = default!;
    public City City { get; set; } = default!;
}
