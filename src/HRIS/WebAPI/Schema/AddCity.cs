namespace Nova.HRIS.Schema;

public static class AddCity
{
    public record Request(string Name, short? ProvinceId);

    public record Response(short Id);
}