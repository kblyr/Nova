namespace Nova.Contracts;

public record VerifyEmailCommand : IRequest
{
    public string EmailAddress { get; init; } = "";
    public string VerificationCode { get; init; } = "";

    public record Response : IResponse
    {
        public static readonly Response Instance = new();
    }
}

public record VerifyEmailRequestedEvent : INotification
{
    public string EmailAddress { get; init; } = "";
    public string VerificationCode { get; init; } = "";
}