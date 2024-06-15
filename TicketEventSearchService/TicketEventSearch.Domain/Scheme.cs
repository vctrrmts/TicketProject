namespace TicketEventSearch.Domain;

public class Scheme
{
    public Guid SchemeId { get; private set; }
    public Guid LocationId { get; private set; }
    public string Name { get; private set;}
    public bool IsActive { get; private set; }
    public virtual Location? Location { get; private set; }
    public virtual ICollection<Seat> Seats { get; private set; }

    private Scheme() { }
    
    public Scheme(Guid schemeId, Guid locationId, string name) 
    {
        #region schemeId validation
        if (string.IsNullOrWhiteSpace(schemeId.ToString()))
        {
            throw new ArgumentException("Wrong SchemeId", nameof(schemeId));
        }
        #endregion

        #region locationId validation
        if (string.IsNullOrWhiteSpace(locationId.ToString()))
        {
            throw new ArgumentException("Wrong LocationId", nameof(locationId));
        }
        #endregion

        #region name validation
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException("Name is empty", nameof(name));
        }

        if (name.Length > 100)
        {
            throw new ArgumentException("Name length more then 100", nameof(name));
        }
        #endregion

        SchemeId = schemeId;
        LocationId = locationId;
        Name = name;
        IsActive = true;
    }

    public void UpdateName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException("Name is empty", nameof(name));
        }

        if (name.Length > 100)
        {
            throw new ArgumentException("Name length more then 100", nameof(name));
        }

        Name = name;
    }

    public void UpdateIsActive(bool isActive)
    {
        IsActive = isActive;
    }

}
