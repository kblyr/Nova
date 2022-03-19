using System.Reflection;

namespace Nova.HRIS.EFCore;

public static class AssemblyMarker
{
    public static readonly Assembly Assembly = typeof(AssemblyMarker).Assembly;
}