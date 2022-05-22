namespace Nova.Identity.Schema;

public static class VerifyUserEmail
{
    [SchemaId(RequestSchemaIds.VerifyUserEmail)]
    public record Request : IApiRequest
    {
        public string UserId { get; init; } = "";
        public string EmailAddress { get; init; } = "";
        public string VerificationCode { get; init; } = "";
    }

    [SchemaId(ResponseSchemaIds.VerifyUserEmail)]
    public record Response : IApiResponse
    {
    }
}