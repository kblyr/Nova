namespace Nova.HRIS.Contracts;

public record BarangayNotInCity(short BarangayId, short? CityId) : FailedResponse;