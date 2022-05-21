using HashidsNet;

namespace Nova;

public interface IHashIdConverter { }

public interface IHashIdConverter<T> : IHashIdConverter 
{
    string Convert(T id);
    T Convert(string hashId);
}

public abstract class HashIdConverterBase
{
    readonly IHashids _hashids;
    protected IHashids Hashids => _hashids;

    protected HashIdConverterBase(string salt)
    {
        _hashids = new Hashids(salt);
    }
}

public abstract class Int32HashIdConverterBase : HashIdConverterBase
{
    protected Int32HashIdConverterBase(string salt) : base(salt)
    {
    }

    public string Convert(int id)
    {
        return Hashids.Encode(id);
    }

    public int Convert(string hashId)
    {
        return Hashids.DecodeSingle(hashId);
    }
}