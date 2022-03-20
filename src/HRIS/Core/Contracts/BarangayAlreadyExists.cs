namespace Nova.HRIS.Contracts;

public record BarangayAlreadyExists(string Name, short? CityId) : FailedResponse;