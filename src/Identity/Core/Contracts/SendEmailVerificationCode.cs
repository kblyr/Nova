namespace Nova.Identity.Contracts;

public record SendEmailVerificationCodeCommand : IRequest
{
    public string EmailAddress { get; init; } = "";
    public string VerificationCode { get; init; } = "";

    public record Response : IResponse
    {
        public static readonly Response Instance = new();
    }
}

public record SendEmailVerificationCodeRequestedEvent : INotification
{
    public string EmailAddress { get; init; } = "";
    public string VerificationCode { get; init; } = "";
}