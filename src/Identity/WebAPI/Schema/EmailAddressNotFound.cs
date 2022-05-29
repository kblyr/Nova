namespace Nova.Identity.Schema;

public static class EmailAddressNotFound
{
    [SchemaId(ResponseSchemaIds.EmailAddressNotFound)]
    public record Response : IApiFailedResponse
    {
        public string EmailAddress { get; init; } = "";
    }
}