using System.Runtime.Serialization;

namespace Nova;

public class NovaException : Exception
{
    public NovaException()
    {
    }

    public NovaException(string? message) : base(message)
    {
    }

    public NovaException(string? message, Exception? innerException) : base(message, innerException)
    {
    }

    protected NovaException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }
}