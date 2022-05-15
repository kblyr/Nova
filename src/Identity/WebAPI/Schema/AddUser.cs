namespace Nova.Identity.Schema;

public static class AddUser
{
    [SchemaId(SchemaIds.User.Add.Request)]
    public record Request : IApiRequest
    {
        public string Username { get; init; } = "";
        public string EmailAddress { get; init; } = "";
        public string Password { get; init; } = "";
        public short StatusId { get; init; }
    }

    [SchemaId(SchemaIds.User.Add.Response)]
    public record Response : IApiResponse
    {
        public string Id { get; init; } = "";
    }
}