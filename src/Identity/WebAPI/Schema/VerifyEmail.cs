namespace Nova.Identity.Schema;

public static class VerifyEmail
{
    [SchemaId(RequestSchemaIds.VerifyEmail)]
    public record Request : IApiRequest
    {
        public string EmailAddress { get; init; } = "";
        public string VerificationCode { get; init; } = "";
    }

    [SchemaId(ResponseSchemaIds.VerifyEmail)]
    public record Response : IApiResponse
    {
        public static readonly Response Instance = new();
    }
}