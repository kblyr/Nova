namespace Nova.HRIS.Contracts;

public record CitizenshipNotFoundResponse : IFailedResponse
{
    public int Id { get; init; }
}