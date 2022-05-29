namespace Nova.HRIS.Contracts;

public record EmployeeAlreadyExistsResponse : IFailedResponse
{
    public string FirstName { get; init; } = "";
    public string? MiddleName { get; init; }
    public string LastName { get; init; } = "";
    public string? NameSuffix { get; init; } = "";
    public DateTime BirthDate { get; init; }
}