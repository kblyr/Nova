namespace Nova.Identity.Schema;

public static class UserEmailAddressAlreadyExists
{
    [SchemaId(ResponseSchemaIds.UserEmailAddressAlreadyExists)]
    public record Response : IApiFailedResponse
    {
        public string EmailAddres { get; init; } = "";
    }
}