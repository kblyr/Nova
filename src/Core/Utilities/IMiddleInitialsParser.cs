using System.Text;
namespace Nova.Utilities;

public interface IMiddleInitialsParser
{
    string Parse(string value);
}

sealed class MiddleInitialsParser : IMiddleInitialsParser
{
    static readonly char[] _separators = new[] { ' ', '.' };

    public string Parse(string middleName)
    {
        if (string.IsNullOrWhiteSpace(middleName))
            return "";

        var builder = new StringBuilder();
        var chunks = middleName.Split(_separators, StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
        
        if (chunks.Length > 0)
        {
            for (int i = 0; i < chunks.Length; i++)
            {
                for (int j = 0; j < chunks[i].Length; j++)
                {
                    var character = chunks[i][j];
                    if (char.IsLetter(character))
                    {
                        builder.Append(character);
                        break;
                    }       
                }
            }
        }

        return builder.ToString();
    }
}