using TicketEventSearch.Domain;

namespace TicketEventSearch.Domain;

public class City
{
    public Guid CityId { get; private set; }
    public string Name { get; private set; }
    public bool IsActive { get; private set; }
    public virtual ICollection<Location> Locations { get; private set; }

    private City() { }

    public City(Guid cityId, string name, bool isActive)
    {
        if (string.IsNullOrEmpty(cityId.ToString()))
        {
            throw new ArgumentException("Incorrect cityId", nameof(cityId));
        }

        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException("Name is empty", nameof(name));
        }

        if (name.Length > 50)
        {
            throw new ArgumentException("Name length more then 50", nameof(name));
        }

        CityId = cityId;
        Name = name;
        IsActive = isActive;
    }

    public void UpdateName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException("Name is empty", nameof(name));
        }

        if (name.Length > 50)
        {
            throw new ArgumentException("Name length more then 50", nameof(name));
        }

        Name = name;
    }

    public void UpdateIsActive(bool isActive)
    {
        IsActive = isActive;
    }
}
