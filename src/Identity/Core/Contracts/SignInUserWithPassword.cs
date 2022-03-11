namespace Nova.Identity.Contracts;

public record SignInUserWithPassword(int Id, string Password) : Request
{
    public record Response : Messaging.Response
    {
        public static Response Instance = new();
    }
}