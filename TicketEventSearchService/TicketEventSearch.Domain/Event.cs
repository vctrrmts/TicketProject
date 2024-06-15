using System.Text.RegularExpressions;

namespace TicketEventSearch.Domain;

public class Event
{
    public Guid EventId { get; private set; }
    public Guid LocationId { get; private set; }
    public Guid CategoryId { get; private set; }
    public string Name { get; private set; }
    public string Description { get; private set; }
    public string UriMainImage { get; private set; }
    public DateTime DateTimeEventStart { get; private set; }
    public DateTime DateTimeEventEnd { get; private set; }
    public bool IsActive { get; private set; }
    public virtual ICollection<Ticket> Tickets { get; private set; }
    public virtual Location Location { get; private set; }
    public virtual Category Category { get; private set; }

    private Event() { }

    public Event(Guid eventId, Guid locationId, Guid categoryId, string name, string description, 
        string uriMainImage, DateTime dateTimeEventStart, DateTime dateTimeEventEnd, bool isActive)
    {
        #region eventId validation
        if (string.IsNullOrWhiteSpace(locationId.ToString()))
        {
            throw new ArgumentException("LocationId is empty", nameof(locationId));
        }
        #endregion

        #region locationId validation
        if (string.IsNullOrWhiteSpace(locationId.ToString()))
        {
            throw new ArgumentException("LocationId is empty", nameof(locationId));
        }
        #endregion

        #region categoryId validation
        if (string.IsNullOrWhiteSpace(categoryId.ToString()))
        {
            throw new ArgumentException("CategoryId is empty", nameof(categoryId));
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

        #region description validation
        if (string.IsNullOrWhiteSpace(description))
        {
            throw new ArgumentException("Description is empty", nameof(description));
        }

        if (description.Length > 1000)
        {
            throw new ArgumentException("Place length more then 1000", nameof(description));
        }
        #endregion

        #region uriMainImage validation
        if (!IsValidURL(uriMainImage))
        {
            throw new ArgumentException("Incorrect Main Image URL", nameof(uriMainImage));
        }
        #endregion

        #region dateTimeEventStart validation
        if (dateTimeEventStart < DateTime.UtcNow)
        {
            throw new ArgumentException("Incorrect Event Start DateTime", nameof(dateTimeEventStart));
        }
        #endregion

        #region dateTimeEventEnd validation
        if (dateTimeEventEnd < DateTime.UtcNow)
        {
            throw new ArgumentException("Incorrect Event End DateTime", nameof(dateTimeEventEnd));
        }
        #endregion

        EventId = eventId;
        LocationId = locationId;
        CategoryId = categoryId;
        Name = name;
        Description = description;
        UriMainImage = uriMainImage;
        DateTimeEventStart = dateTimeEventStart;
        DateTimeEventEnd = dateTimeEventEnd;
        IsActive = isActive;
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

    public void UpdateDescription(string description)
    {
        if (string.IsNullOrWhiteSpace(description))
        {
            throw new ArgumentException("Description is empty", nameof(description));
        }

        if (description.Length > 1000)
        {
            throw new ArgumentException("Place length more then 100", nameof(description));
        }

        Description = description;
    }

    public void UpdateDateTimeEventStart(DateTime dateTimeEventStart)
    {
        if (dateTimeEventStart < DateTime.UtcNow)
        {
            throw new ArgumentException("Incorrect Event Start DateTime", nameof(dateTimeEventStart));
        }

        DateTimeEventStart = dateTimeEventStart;
    }

    public void UpdateDateTimeEventEnd(DateTime dateTimeEventEnd)
    {
        if (dateTimeEventEnd < DateTime.UtcNow)
        {
            throw new ArgumentException("Incorrect Event End DateTime", nameof(dateTimeEventEnd));
        }

        DateTimeEventEnd = dateTimeEventEnd;
    }

    public void UpdateUriMainImage(string uriMainImage)
    {
        if (!IsValidURL(uriMainImage))
        {
            throw new ArgumentException("Incorrect Main Image URL", nameof(uriMainImage));
        }
    }

    public void UpdateIsActive(bool isActive)
    {
        IsActive = isActive;
    }

    private bool IsValidURL(string URL)
    {
        string Pattern = @"^(?:http(s)?:\/\/)?[\w.-]+(?:\.[\w\.-]+)+[\w\-\._~:/?#[\]@!\$&'\(\)\*\+,;=.]+$";
        Regex Rgx = new Regex(Pattern, RegexOptions.Compiled | RegexOptions.IgnoreCase);
        return Rgx.IsMatch(URL);
    }
}
