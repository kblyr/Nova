#nullable disable

namespace Nova.Identity.Entities;

public class UserPasswordLogin : UserLogin
{
    public string HashedPassword { get; set; }
}