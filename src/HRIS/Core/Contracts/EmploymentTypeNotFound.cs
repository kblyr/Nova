namespace Nova.HRIS.Contracts;

public record EmploymentTypeNotFoundResponse : IFailedResponse
{
    public short Id { get; init; }
}