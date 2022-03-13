namespace Nova.Identity.Entities;

public class UserStatus
{
    public short Id { get; set; }
    public string Name { get; set; }

    public IEnumerable<User> Users { get; set; }
}