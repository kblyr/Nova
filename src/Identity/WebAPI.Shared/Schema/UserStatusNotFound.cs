namespace Nova.Identity.Schema;

public static class UserStatusNotFound
{
    [SchemaId(SchemaIds.UserStatusNotFound.Response)]
    public record Response(string Id) : IApiFailedResponse;
}