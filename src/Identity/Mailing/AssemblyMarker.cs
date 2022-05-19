using System.Reflection;

namespace Nova.Identity.Mailing;

public static class AssemblyMarker 
{
    public static readonly Assembly Assembly = typeof(AssemblyMarker).Assembly; 
}