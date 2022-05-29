namespace Nova.HRIS.Contracts;

public record AddEmployeeCommand : IRequest
{
    public string FirstName { get; init; } = "";
    public string? MiddleName { get; init; }
    public string LastName { get; init; } = "";
    public string? NameSuffix { get; init; }
    public string? MaidenMiddleName { get; init; }
    public bool Sex { get; init; }
    public DateTime BirthDate { get; init; }
    public string? BirthPlace { get; init; }
    public short CivilStatusId { get; init; }
    public string? LandlineNumber { get; init; }
    public string? MobileNumber { get; init; }
    public string? EmailAddress { get; init; }
    public int CitizenshipId { get; init; }
    public short EmploymentStatusId { get; init; }
    public string? TIN { get; init; }
    public string? SSSNumber { get; init; }
    public string? GSISNumber { get; init; }
    public string? PagibigNumber { get; init; }
    public string? PhilhealthNumber { get; init; }
    public AddressObj? ResidentialAddress { get; init; }
    public AddressObj? PermanentAddress { get; init; }
    public EmploymentObj? Employment { get; init; }
    public SalaryGradeStepObj? SalaryGradeStep { get; init; }

    public record AddressObj
    {
        public string? UnitRoomNumber { get; set; }
        public string? HouseNumber { get; set; }
        public string? Building { get; set; }
        public string? BlockNumber { get; set; }
        public string? LotNumber { get; set; }
        public string? PhaseNumber { get; set; }
        public string? Street { get; set; }
        public string? SubdivisionVillage { get; set; }
        public int? BarangayId { get; set; }
        public int? CityId { get; set; }
        public int? ProvinceId { get; set; }
        public string? ZipCode { get; set; }
    }

    public record EmploymentObj
    {
        public short TypeId { get; init; }
        public int OfficeId { get; init; }
        public int PositionId { get; init; }
        public decimal? Salary { get; init; }
        public DateTimeOffset EffectivityBeginDate { get; init; }
        public DateTimeOffset? EffectivityEndDate { get; init; }
    }

    public record SalaryGradeStepObj
    {
        public short Grade { get; init; }
        public short Step { get; init; }
        public DateTimeOffset EffectivityBeginDate { get; init; }
        public DateTimeOffset? EffectivityEndDate { get; init; }
    }

    public record Response : IResponse
    {
        public int Id { get; init; }
    }
}