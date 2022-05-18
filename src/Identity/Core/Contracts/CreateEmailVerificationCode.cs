namespace Nova.Contracts;

public record CreateEmailVerificationCodeCommand : IRequest
{
    public string EmailAddress { get; init; } = "";

    public record Response : IResponse
    {
        public string VerificationCode { get; init; } = "";
    }
}

public record CreateEmailVerificationCodeRequestedEvent : INotification
{
    public string EmailAddress { get; init; } = "";
}