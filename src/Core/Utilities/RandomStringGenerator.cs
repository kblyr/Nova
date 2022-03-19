using System.Security.Cryptography;
using System.Text;

namespace Nova.Utilities;

public sealed class RandomStringGenerator
{
    static readonly char[] _chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890".ToCharArray();

    public string Generate(int size)
	{            
		byte[] data = new byte[4*size];
		using var crypto = RandomNumberGenerator.Create();
        crypto.GetBytes(data);
		var result = new StringBuilder(size);
        
		for (int i = 0; i < size; i++)
		{
			var rnd = BitConverter.ToUInt32(data, i * 4);
			var idx = rnd % _chars.Length;
			result.Append(_chars[idx]);
		}

		return result.ToString();
	}
}