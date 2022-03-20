namespace Nova.HRIS.Contracts;

public record CityAlreadyExists(string Name, short? ProvinceId) : FailedResponse;