using System.Reflection;

namespace Nova.Identity.Core;

public static class AssemblyMarker
{
    public static readonly Assembly Assembly = typeof(AssemblyMarker).Assembly;
}