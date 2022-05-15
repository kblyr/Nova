using System.Security.Cryptography;
using System.Text;

namespace Nova.Utilities;

public interface IRandomStringGenerator
{
    string Generate(int length);
    string Generate(int length, char[] chars);
    string Generate(int length, string charsString);
}

sealed class RandomStringGenerator : IRandomStringGenerator
{
    static readonly int _blockSize = 4;
    static readonly char[] _chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890".ToCharArray();

    public string Generate(int length)
    {
        return Generate(length, _chars);
    }

    public string Generate(int length, char[] chars)
    {
        if (length <= 0 || chars is null || chars.Length == 0)
        {
            return "";
        }

        var randomData = RandomNumberGenerator.GetBytes(_blockSize * length);
        var builder = new StringBuilder();

        for (var i = 0; i < length; i++)
        {
            builder.Append(chars[BitConverter.ToUInt32(randomData, _blockSize * i) % chars.Length]);
        }

        return builder.ToString();
    }

    public string Generate(int length, string charsString)
    {
        if (string.IsNullOrWhiteSpace(charsString))
        {
            return "";
        }

        return Generate(length, charsString.ToCharArray());
    }
}