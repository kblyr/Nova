namespace Nova.HRIS.Contracts;

public record PositionNotFoundResponse : IFailedResponse
{
    public int Id { get; init; }
}