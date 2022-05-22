namespace Nova.Identity.Schema;

public static class UserEmailAddressNotFound
{
    [SchemaId(ResponseSchemaIds.UserEmailAddressNotFound)]
    public record Response : IApiFailedResponse
    {
        public string UserId { get; init; } = "";
        public string EmailAddress { get; init; } = "";
    }
}