namespace Nova.Identity.Contracts;

public record VerifyUserEmailCommand : IRequest
{
    public int UserId { get; init; }
    public string EmailAddress { get; init; } = "";
    public string VerificationCode { get; init; } = "";

    public record Response : IResponse
    {
        public int UserId { get; init; }
        public string EmailAddress { get; init; } = "";
    }
}