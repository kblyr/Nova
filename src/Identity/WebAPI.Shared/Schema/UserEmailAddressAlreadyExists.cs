namespace Nova.Identity.Schema;

public static class UserEmailAddressAlreadyExists
{
    public record Response(string EmailAddress);
}