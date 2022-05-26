namespace Nova.HRIS.Configuration;

public record CivilStatusesLookup
{
    public short Single { get; init; } = 1;
    public short Married { get; init; } = 2;
    public short WidowWidower { get; init; } = 3;
    public short Annulled { get; init; } = 4;
    public short Separated { get; init; } = 5;
}