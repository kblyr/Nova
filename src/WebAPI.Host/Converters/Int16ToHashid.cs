using AutoMapper;
using HashidsNet;

namespace Nova.Converters;

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