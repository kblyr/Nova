namespace Nova.Identity.Schema;

public static class IncorrectUserEmailVerificationCode
{
    [SchemaId(ResponseSchemaIds.IncorrectUserEmailVerificationCode)]
    public record Response : IApiFailedResponse
    {
        public string UserId { get; init; } = "";
        public string EmailAddress { get; init; } = "";
        public string VerificationCode { get; init; } = "";
    }
}