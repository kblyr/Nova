#nullable disable

namespace Nova.Identity.Entities;

public record UserStatus
{
    public short Id { get; set; }
    public string Name { get; set; }
}