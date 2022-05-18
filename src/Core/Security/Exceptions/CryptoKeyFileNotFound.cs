using System.Runtime.Serialization;

namespace Nova.Security.Exceptions;

public class CryptoKeyFileNotFoundException : FileNotFoundException
{
    public CryptoKeyFileNotFoundException()
    {
    }

    public CryptoKeyFileNotFoundException(string? message) : base(message)
    {
    }

    public CryptoKeyFileNotFoundException(string? message, Exception? innerException) : base(message, innerException)
    {
    }

    public CryptoKeyFileNotFoundException(string? message, string? fileName) : base(message, fileName)
    {
    }

    public CryptoKeyFileNotFoundException(string? message, string? fileName, Exception? innerException) : base(message, fileName, innerException)
    {
    }

    protected CryptoKeyFileNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }
}