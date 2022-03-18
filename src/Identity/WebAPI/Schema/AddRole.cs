namespace Nova.Identity.Schema;

public static class AddRole
{
    public record Request(string Name, short? DomainId, short? ApplicationId);

    public record Response(int Id, string Code);
}