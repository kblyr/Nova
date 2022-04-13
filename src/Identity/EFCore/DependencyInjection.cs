using System.Reflection;

namespace Nova.Identity.EFCore;

public static class AssemblyMarker
{
    public static readonly Assembly Assembly = typeof(AssemblyMarker).Assembly;
}