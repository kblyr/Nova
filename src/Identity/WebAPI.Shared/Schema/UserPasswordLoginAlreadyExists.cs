namespace Nova.Identity.Schema;

public static class UserPasswordLoginAlreadyExists
{
    [SchemaId(SchemaIds.UserPasswordLoginAlreadyExists.Response)]
    public record Response(string UserId) : IApiFailedResponse;
}