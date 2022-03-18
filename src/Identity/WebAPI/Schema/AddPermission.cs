namespace Nova.Identity.Schema;

public static class AddPermission
{
    public record Request(string Name, short? DomainId, short? ApplicationId);

    public record Response(int Id, string Code);
}