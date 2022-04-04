namespace Nova.Identity.Configuration;

public record UserStatusesConfig
{
    public const string CONFIGKEY = "Nova:Identity:UserStatuses";

    public short Pending { get; init; }
    public short Active { get; init; }
    public short Deactivated { get; init; }
    public short Locked { get; init; }
}