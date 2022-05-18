namespace Nova.Converters;

sealed class UserIdConverter : IHashIdConverter<int>
{
    readonly IHashids _hashids;

    public UserIdConverter(IHashids hashids)
    {
        _hashids = hashids;
    }

    public string Convert(int id)
    {
        return _hashids.Encode(id);
    }

    public int Convert(string hashId)
    {
        return _hashids.DecodeSingle(hashId);
    }
}
