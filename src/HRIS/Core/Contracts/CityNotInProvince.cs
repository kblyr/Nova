namespace Nova.HRIS.Contracts;

public record CityNotInProvince(short CityId, short? ProvinceId) : FailedResponse;