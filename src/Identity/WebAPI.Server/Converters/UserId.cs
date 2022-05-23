namespace Nova.Identity.Converters;

public sealed class UserIdConverter : Int32HashIdConverterBase, IHashIdConverter<int>
{
    public UserIdConverter(string salt, int minHashLength) : base(salt, minHashLength)
    {
    }
}
