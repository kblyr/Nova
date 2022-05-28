namespace Nova.HRIS.Configuration;

public record EmploymentTypesLookup
{
    public const string CONFIGKEY = "Nova:HRIS:EmploymentTypes";

    public short Regular { get; init; } = 1;
    public short ContractOfService { get; init; } = 2;
    public short Coterminous { get; init; } = 3;
    public short JobOrder { get; init; } = 4;
}