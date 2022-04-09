using HashidsNet;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Nova;

public static class IHashidsExtensions
{
    public static int DecodeFirstOrDefault(this IHashids hashids, string hash, int? fallback = null)
    {
        var result = hashids.Decode(hash);
        return result.Length > 0 ? result[0] : fallback ?? default;
    }
}