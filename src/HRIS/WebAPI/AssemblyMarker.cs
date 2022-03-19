using System.Reflection;

namespace Nova.HRIS.WebAPI;

public static class AssemblyMarker
{
    public static readonly Assembly Assembly = typeof(AssemblyMarker).Assembly;
}