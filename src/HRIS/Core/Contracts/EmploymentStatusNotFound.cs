namespace Nova.HRIS.Contracts;

public record EmploymentStatusNotFoundResponse : IFailedResponse
{
    public short Id { get; init; }
}