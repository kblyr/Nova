namespace Nova.Identity.Schema;

public static class EmailVerificationCodeAlreadyCreated
{
    [SchemaId(ResponseSchemaIds.EmailVerificationCodeAlreadyCreated)]
    public record Response : IApiFailedResponse
    {
        public string EmailAddress { get; init; } = "";
    }
}