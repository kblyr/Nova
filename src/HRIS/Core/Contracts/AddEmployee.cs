namespace Nova.HRIS.Contracts;

public record AddEmployee
(
    string FirstName,
    string? MiddleName,
    string LastName,
    string? NameSuffix,
    string? MaidenMiddleName,
    bool? Sex,
    DateTime? BirthDate,
    string? BirthPlace,
    string? ContactNumber,
    short? CivilStatusId,
    short? NationalityId,
    string Address,
    short? BarangayId,
    short? CityId,
    short? ProvinceId
) : Request
{
    public record Response(int Id) : Messaging.Response;
}