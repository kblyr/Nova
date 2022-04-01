using System.Reflection;

namespace Nova.HRIS.Core;

public static class AssemblyMarker
{
    public static readonly Assembly Assembly = typeof(AssemblyMarker).Assembly;
}