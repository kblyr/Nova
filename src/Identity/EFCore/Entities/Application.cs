namespace Nova.Identity.Entities;

public class Application
{
    public short Id { get; set; }
    public string Name { get; set; }
    public short? DomainId { get; set; }

    public Domain Domain { get; set; }
}