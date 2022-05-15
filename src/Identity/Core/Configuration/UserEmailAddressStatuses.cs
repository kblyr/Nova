namespace Nova.Identity.Configuration;

public record UserEmailAddressStatusesOptions
{
    public const string CONFIGKEY = "Nova:Identity:UserEmailAddressStatuses";

    public short Pending { get; init; } = 1;
    public short Verified { get; init; } = 2;
}