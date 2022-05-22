namespace Nova.Identity.Contracts;

public record CreateUserEmailVerificationCodeCommand : IRequest
{
    public int UserId { get; init; }
    public string EmailAddress { get; init; } = "";

    public record Response : IResponse
    {
        public string VerificationCode { get; init; } = "";
    }
}

public record CreateUserEmailVerificationCodeRequestedEvent : INotification
{
    public int UserId { get; init; }
    public string EmailAddress { get; init; } = "";
}