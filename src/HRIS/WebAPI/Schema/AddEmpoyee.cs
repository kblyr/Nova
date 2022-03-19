namespace Nova.HRIS.Schema;

public static class AddEmployee
{
    public record Request
    (
        string FirstName,
        string MiddleName,
        string LastName,
        string NameSuffix,
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
    );

    public record Response(int Id);
}