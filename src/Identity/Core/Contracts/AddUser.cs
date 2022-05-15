namespace Nova.Identity.Contracts;

public record AddUserCommand : IRequest
{
    public string Username { get; init; } = "";
    public string EmailAddress { get; init; } = "";
    public string? Password { get; init; }
    public short StatusId { get; init; }

    public record Response : IResponse
    {
        public int Id { get; init; }
    }
}