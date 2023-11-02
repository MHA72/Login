namespace Login.Models.User;

public sealed class UserLogin : BaseEntity
{
    public UserLogin(Guid userId)
    {
        UserId = userId;
    }

    public UserLogin()
    {
    }

    public Guid UserId { get; set; }
    public Login.Models.User.User? User { get; set; }
}