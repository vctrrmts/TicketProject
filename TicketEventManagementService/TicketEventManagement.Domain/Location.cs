namespace TicketEventManagement.Domain;

public class Location
{
    public Guid LocationId { get; private set; }
    public Guid CityId { get; private set; }
    public string Name { get; private set; }
    public string Address { get; private set; }
    public double Latitude { get; private set; }
    public double Longitude { get; private set;}
    public bool IsActive { get; private set; }
    public virtual City? City { get; private set; }
    public virtual ICollection<Scheme> Schemes { get; private set; }

    private Location() { }

    public Location(Guid cityId, string name, string address, double latitude, double longitude)
    {
        #region cityId validation
        if (string.IsNullOrWhiteSpace(CityId.ToString()))
        {
            throw new ArgumentException("Wrong CityId", nameof(cityId));
        }
        #endregion

        #region name validation
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException("Name is empty", nameof(name));
        }

        if (name.Length > 200)
        {
            throw new ArgumentException("Name length more then 200", nameof(name));
        }
        #endregion

        #region address validation
        if (string.IsNullOrWhiteSpace(address))
        {
            throw new ArgumentException("Address is empty", nameof(address));
        }

        if (address.Length > 200)
        {
            throw new ArgumentException("Address length more then 200", nameof(address));
        }
        #endregion

        #region latidute validation
        if (latitude < -90 || latitude > 90)
        {
            throw new ArgumentException("Latidute must be between -90 and 90 degrees inclusive.", nameof(latitude));
        }
        #endregion

        #region longitude validation
        if (longitude < -180 || longitude > 180)
        {
            throw new ArgumentException("Longitude must be between -180 and 180 degrees inclusive.", nameof(longitude));
        }
        #endregion

        CityId = cityId;
        Name = name;
        Address = address;
        Latitude = latitude;
        Longitude = longitude;
        IsActive = true;
    }

    public void UpdateName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException("Name is empty", nameof(name));
        }

        if (name.Length > 200)
        {
            throw new ArgumentException("Name length more then 200", nameof(name));
        }

        Name = name;
    }

    public void UpdateAddress(string address) 
    {
        if (string.IsNullOrWhiteSpace(address))
        {
            throw new ArgumentException("Address is empty", nameof(address));
        }

        if (address.Length > 200)
        {
            throw new ArgumentException("Address length more then 200", nameof(address));
        }

        Address = address;
    }

    public void UpdateLatitude(double latitude) 
    {
        if (latitude < -90 || latitude > 90)
        {
            throw new ArgumentException("Latidute must be between -90 and 90 degrees inclusive.", nameof(latitude));
        }
        Latitude = latitude;
    }

    public void UpdateLongitude(double longitude)
    {
        if (longitude < -180 || longitude > 180)
        {
            throw new ArgumentException("Longitude must be between -180 and 180 degrees inclusive.", nameof(longitude));
        }
        Longitude = longitude;
    }

    public void UpdateIsActive(bool isActive)
    {
        IsActive = isActive;
    }
}
