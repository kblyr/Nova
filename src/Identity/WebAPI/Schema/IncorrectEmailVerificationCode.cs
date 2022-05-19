namespace Nova.Identity.Schema;

public static class IncorrectEmailVerificationCode
{
    [SchemaId(ResponseSchemaIds.IncorrectEmailVerificationCode)]
    public record Response : IApiFailedResponse
    {
        public string EmailAddress { get; init; } = "";
        public string VerificationCode { get; init; } = "";
    }
}