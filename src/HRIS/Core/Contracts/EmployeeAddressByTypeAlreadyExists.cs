namespace Nova.HRIS.Contracts;

public record EmployeeAddressByTypeAlreadyExistsResponse : IFailedResponse
{
    public int EmployeeId { get; init; }
    public short AddressTypeId { get; init; }
}