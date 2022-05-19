namespace Nova.Identity.Contracts;

public record EmailAddressNotFoundResponse : IFailedResponse
{
    public string EmailAddress { get; init; } = "";
}