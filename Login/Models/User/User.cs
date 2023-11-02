namespace Login.Models.User;

public sealed class User : BaseEntity
{
    private User()
    {
    }

    public string Email { get; set; }
    public string Username { get; set; }
    public string PasswordHash { get; set; }
    public ICollection<UserLogin>? UserLogins { get; set; }
    
    public static User Create(string username, string email)
    {
        return new User
        {
            Username = username,
            Email = email,
        };
    }
}