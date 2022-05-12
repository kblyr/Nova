namespace Nova.Results;

public record SmtpClientConnectedResult : IResult
{
    public static readonly SmtpClientConnectedResult Instance = new();
}