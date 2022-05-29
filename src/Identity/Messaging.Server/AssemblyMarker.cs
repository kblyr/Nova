using System.Reflection;

namespace Nova.Identity.Messaging.Server;

public static class AssemblyMarker 
{
    public static readonly Assembly Assembly = typeof(AssemblyMarker).Assembly; 
}