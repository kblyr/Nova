using System.Reflection;

namespace Nova.Identity.WebAPI.Server;

public static class AssemblyMarker 
{
    public static readonly Assembly Assembly = typeof(AssemblyMarker).Assembly; 
}