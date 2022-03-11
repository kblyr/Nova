namespace Nova.Identity.Contracts;

public record IncorrectUserPassword : FailedResponse
{
    public static readonly IncorrectUserPassword Instance = new();
}