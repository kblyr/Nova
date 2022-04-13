using System.Reflection;

namespace Nova.Identity.EFCore.Postgres;

public static class AssemblyMarker
{
    public static readonly Assembly Assembly = typeof(AssemblyMarker).Assembly;
}