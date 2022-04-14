namespace Nova.Identity.Schema;

public static class UserEmailAddressAlreadyExists
{
    [SchemaId(SchemaIds.UserEmailAddressAlreadyExists.Response)]
    public record Response(string EmailAddress);
}