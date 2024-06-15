namespace Authorization.Domain;

public class User
{
    public Guid UserId { get; set; }
    public string Login { get; set; } = default!;
    public string PasswordHash { get; set; } = default!;
}
