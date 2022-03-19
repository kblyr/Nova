using System.Reflection;

namespace Nova.Identity.Redis;

public static class AssemblyMarker
{
    public static readonly Assembly Assembly = typeof(AssemblyMarker).Assembly;
}