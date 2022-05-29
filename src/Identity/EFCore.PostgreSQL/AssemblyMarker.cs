using System.Reflection;

namespace Nova.Identity.EFCore.PostgreSQL;

public static class AssemblyMarker 
{
    public static readonly Assembly Assembly = typeof(AssemblyMarker).Assembly; 
}