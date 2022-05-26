namespace Nova.HRIS.Configuration;

public record EmploymentStatusesLookup
{
    public short Active { get; init; } = 1;
    public short Resigned { get; init; } = 2;
    public short Terminated { get; init; } = 3;
    public short Retired { get; init; } = 4;
    public short Awol { get; init; } = 5;
}