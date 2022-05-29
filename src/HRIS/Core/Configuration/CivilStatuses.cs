namespace Nova.HRIS.Configuration;

public record CivilStatusesLookup
{
    public const string CONFIGKEY = "Nova:HRIS:CivilStatuses";

    public short Single { get; init; } = 1;
    public short Married { get; init; } = 2;
    public short Widowed { get; init; } = 3;
    public short Annulled { get; init; } = 4;
    public short Separated { get; init; } = 5;
}