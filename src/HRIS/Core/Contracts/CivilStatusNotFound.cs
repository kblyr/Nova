namespace Nova.HRIS.Contracts;

public record CivilStatusNotFoundResponse : IFailedResponse
{
    public short Id { get; init; }
}