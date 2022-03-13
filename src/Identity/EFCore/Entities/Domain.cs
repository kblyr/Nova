namespace Nova.Identity.Entities;

public class Domain
{
    public short Id { get; set; }
    public string Name { get; set; }

    public IEnumerable<Application> Applications { get; set; }
    public IEnumerable<Role> Roles { get; set; }
    public IEnumerable<Permission> Permissions { get; set; }
}
