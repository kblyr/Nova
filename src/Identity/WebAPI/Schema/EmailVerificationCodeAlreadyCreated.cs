namespace Nova.Identity.Schema;

public static class EmailVerificationCodeAlreadyCreated
{
    [SchemaId(ResponseSchemaIds.EmailVerificationCodeAlreadyCreated)]
    public record Response : IApiResponse
    {
        public string EmailAddress { get; init; } = "";
    }
}