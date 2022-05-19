namespace Nova.Identity.Schema;

public static class CreateEmailVerificationCode
{
    [SchemaId(RequestSchemaIds.CreateEmailVerificationCode)]
    public record Request : IApiRequest
    {
        public string EmailAddress { get; init; } = "";
    }

    [SchemaId(ResponseSchemaIds.CreateEmailVerificationCode)]
    public record Response : IApiResponse
    {
        public string VerificationCode { get; init; } = "";
    }
}