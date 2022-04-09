using AutoMapper;
using HashidsNet;

namespace Nova.Converters;

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