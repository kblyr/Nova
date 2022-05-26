namespace Nova.HRIS.Contracts;

public record AddEmployeeCommand : IRequest
{
    public string FirstName { get; init; } = "";
    public string? MiddleName { get; init; }
    public string LastName { get; init; } = "";
    public string? NameSuffix { get; init; }
    public string? MaidenMiddleName { get; init; }
    public bool Sex { get; init; }
    public DateTime? BirthDate { get; init; }
    public string? BirthPlace { get; init; }
    public short CivilStatusId { get; init; }
    public string? ContactNumber { get; init; }

    public record EmploymentObj
    {
        public int OfficeId { get; init; }
        public short TypeId { get; init; }
        public int PositionId { get; init; }
        public decimal MonthlySalary { get; init; }
        public DateTimeOffset EffectivityDateBegin { get; init; }
        public DateTimeOffset? EffectivityDateEnd { get; init; }
    }

    public record Response : IResponse
    {
        public int Id { get; init; }
    }
}