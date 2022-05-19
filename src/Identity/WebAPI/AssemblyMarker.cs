using System.Reflection;

namespace Nova.Identity.WebAPI;

public static class AssemblyMarker 
{
    public static readonly Assembly Assembly = typeof(AssemblyMarker).Assembly; 
}