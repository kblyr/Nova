namespace Nova.Identity.Contracts;

public record AddPermission(string Name, short? DomainId, short? ApplicationId) : Request
{
    public record Response(int Id, string Code) : Messaging.Response;
}