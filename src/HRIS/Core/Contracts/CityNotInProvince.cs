namespace Nova.HRIS.Contracts;

public record CityNotInProvinceResponse : IFailedResponse
{
    public int CityId { get; init; }
    public int? ProvinceId { get; init; }
}