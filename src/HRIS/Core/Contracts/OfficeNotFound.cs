namespace Nova.HRIS.Contracts;

public record OfficeNotFoundResponse : IFailedResponse
{
    public int Id { get; init; }
}