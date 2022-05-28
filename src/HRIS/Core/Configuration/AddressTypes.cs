namespace Nova.HRIS.Configuration;

public record AddressTypesLookup
{
    public const string CONFIGKEY = "Nova:HRIS:AddressTypes";

    public short Permanent { get; init; } = 1;
    public short Residential { get; init; } = 2;
}