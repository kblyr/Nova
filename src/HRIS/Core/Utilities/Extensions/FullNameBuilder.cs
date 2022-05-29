#nullable disable
using Nova.Utilities;

namespace Nova.HRIS.Utilities;

public static class IFullNameBuilderExtensions
{
    public static string Build(this IFullNameBuilder builder, IFullNameSource source)
    {
        if (source is null)
        {
            return "";
        }

        return builder.Build(source.FirstName, source.MiddleName, source.LastName, source.NameSuffix);
    }
}

public interface IFullNameSource
{
    string FirstName { get; }
    string MiddleName { get; }
    string LastName { get; }
    string NameSuffix { get; }
}