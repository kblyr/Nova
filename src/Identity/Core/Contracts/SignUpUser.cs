namespace Nova.Identity.Contracts;

public record SignUpUserCommand : IRequest
{
    public string EmailAddress { get; init; } = "";
    public string CipherPassword { get; init; } = "";
    public string FirstName { get; init; } = "";
    public string LastName { get; init; } = "";

    public record Response : IResponse
    {
        public int Id { get; init; }
    }
}