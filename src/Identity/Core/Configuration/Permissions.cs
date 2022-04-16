namespace Nova.Identity.Contracts;

public record PermissionsConfig
{
    public const string CONFIGKEY = "Nova:Identity:User";

    public string AddUser { get; init; } = "";
    public string AddUserEmailAddress { get; init; } = "";
    public string AddUserPasswordLogin { get; init; } = "";
}