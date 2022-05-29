namespace Nova.Identity.Schema;

public static class SignUpUser
{
    [SchemaId(RequestSchemaIds.SignUpUser)]
    public record Request : IApiRequest
    {
        public string EmailAddress { get; init; } = "";
        public string CipherPassword { get; init; } = "";
        public string FirstName { get; init; } = "";
        public string LastName { get; init; } = "";
    }

    [SchemaId(ResponseSchemaIds.SignUpUser)]
    public record Response : IApiResponse
    {
        public string Id { get; init; } = "";
    }
}