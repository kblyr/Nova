namespace Nova.Identity.Schema;

public static class UserPasswordLoginAlreadyExists
{
    public record Response(string UserId) : IApiFailedResponse;
}