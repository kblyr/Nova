namespace Nova.Identity.Schema;

public static class UserNotFound
{
    [SchemaId(SchemaIds.UserNotFound.Response)]
    public record Response(string Id) : IApiFailedResponse;
}