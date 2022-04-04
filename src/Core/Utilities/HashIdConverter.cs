using AutoMapper;
using HashidsNet;

namespace Nova.Utilities;

public sealed class Int16ToHashIdConverter : IValueConverter<short, string>
{
    readonly IHashids _hashids;

    public Int16ToHashIdConverter(IHashids hashids)
    {
        _hashids = hashids;
    }

    public string Convert(short sourceMember, ResolutionContext context)
    {
        return _hashids.Encode(sourceMember);
    }
}

public sealed class Int32ToHashIdConverter : IValueConverter<int, string>
{
    readonly IHashids _hashids;

    public Int32ToHashIdConverter(IHashids hashids)
    {
        _hashids = hashids;
    }

    public string Convert(int sourceMember, ResolutionContext context)
    {
        return _hashids.Encode(sourceMember);
    }
}

public sealed class Int64ToHashIdConverter : IValueConverter<long, string>
{
    readonly IHashids _hashids;

    public Int64ToHashIdConverter(IHashids hashids)
    {
        _hashids = hashids;
    }

    public string Convert(long sourceMember, ResolutionContext context)
    {
        return _hashids.EncodeLong(sourceMember);
    }
}
