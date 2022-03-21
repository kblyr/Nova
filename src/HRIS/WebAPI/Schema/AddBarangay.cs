namespace Nova.HRIS.Schema;

public static class AddBarangay
{
    public record Request(string Name, short? CityId);

    public record Response(short Id);
}