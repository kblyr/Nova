using System.Runtime.Serialization;

namespace Nova;

public class UnsupportedInstanceException : Exception
{
    public Type ExpectedType { get; }
    public Type? ReceivedType { get; }

    public UnsupportedInstanceException(Type expectedType, Type? receivedType)
    {
        ExpectedType = expectedType;
        ReceivedType = receivedType;
    }

    public UnsupportedInstanceException(Type expectedType, Type? receivedType, string? message) : base(message)
    {
        ExpectedType = expectedType;
        ReceivedType = receivedType;
    }

    public UnsupportedInstanceException(Type expectedType, Type? receivedType, string? message, Exception? innerException) : base(message, innerException)
    {
        ExpectedType = expectedType;
        ReceivedType = receivedType;
    }

    protected UnsupportedInstanceException(Type expectedType, Type? receivedType, SerializationInfo info, StreamingContext context) : base(info, context)
    {
        ExpectedType = expectedType;
        ReceivedType = receivedType;
    }

    public static UnsupportedInstanceException CreateInstance<TExpected>(Type? receivedType)
    {
        return new UnsupportedInstanceException(typeof(TExpected), receivedType);
    }
}
