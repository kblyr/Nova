using AutoMapper;
using HashidsNet;

namespace Nova.Converters;

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
