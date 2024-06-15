namespace TicketEventManagement.Domain;

public class Category
{
    public Guid CategoryId { get; private set; }
    public string Name { get; private set; }
    public bool IsActive { get; private set; }
    public virtual ICollection<Event> Events { get; private set; }

    private Category() { }

    public Category(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException("Name is empty", nameof(name));
        }

        if (name.Length > 30)
        {
            throw new ArgumentException("Name length more then 30", nameof(name));
        }

        Name = name;
        IsActive = true;
    }

    public void UpdateName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException("Name is empty", nameof(name));
        }

        if (name.Length > 30)
        {
            throw new ArgumentException("Name length more then 30", nameof(name));
        }

        Name = name;
    }

    public void UpdateIsActive(bool isActive)
    {
        IsActive = isActive;
    }
}
