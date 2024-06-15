using System.Xml.Linq;

namespace UsersManagement.Domain;

public class User
{
    public Guid UserId { get; set; }
    public string Login { get; set; } = default!;
    public string PasswordHash { get; set; } = default!;

    private User() { }

    public User(string login, string passwordHash)
    {
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
