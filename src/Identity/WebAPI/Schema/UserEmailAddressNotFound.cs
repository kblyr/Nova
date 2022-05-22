namespace Nova.Identity.Schema;

public static class UserEmailAddressNotFound
{
    [SchemaId(ResponseSchemaIds.UserEmailAddressNotFound)]
    public record Response : IApiFailedResponse
    {
        public string Id { get; init; } = "";
        public string EmailAddress { get; init; } = "";
    }
}