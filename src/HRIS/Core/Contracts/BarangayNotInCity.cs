namespace Nova.HRIS.Contracts;

public record BarangayNotInCityResponse : IFailedResponse
{
    public int BarangayId { get; init; }
    public int? CityId { get; init; }
}