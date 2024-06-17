using System.Xml.Linq;

namespace Authorization.Domain;

public class User
{
    public Guid UserId { get; private set; }
    public string Login { get; private set; } = default!;
    public string PasswordHash { get; private set; } = default!;

    private User() { }

    public User(string userId, string login, string passwordHash)
    {
        #region userId validation
        if (!Guid.TryParse(userId, out Guid userIdGuid))
        {
            throw new ArgumentException("Incorrect UserId", nameof(userId));
        }
        #endregion

        #region login validation
        if (login.Length < 3)
        {
            throw new ArgumentException("Login length less then 3", nameof(login));
        }

        if (login.Length > 50)
        {
            throw new ArgumentException("Name length more then 50", nameof(login));
        }
        #endregion

        #region passwordHash validation
        if (string.IsNullOrEmpty(passwordHash))
        {
            throw new ArgumentException("PasswordHash is empty", nameof(passwordHash));
        }
        #endregion

        UserId = userIdGuid;
        Login = login;
        PasswordHash = passwordHash;
    }

    public void UpdatePasswordHash(string passwordHash)
    {
        #region passwordHash validation
        if (string.IsNullOrEmpty(passwordHash))
        {
            throw new ArgumentException("PasswordHash is empty", nameof(passwordHash));
        }
        #endregion

        PasswordHash = passwordHash;
    }
}
