namespace Nova.Identity.Contracts;

public record SendEmailVerificationToUserCommand(int Id, string Username, string EmailAddress) : IRequest
{
    public record Response : IResponse
    {
        public static readonly Response Instance = new();
    }
}