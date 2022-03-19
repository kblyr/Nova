using System.Text;
namespace Nova.Utilities;

public interface IFullNameBuilder
{
    string Build(string firstName, string middleName, string lastName, string nameSuffix, string format);
}

sealed class FullNameBuilder : IFullNameBuilder
{
    const string Placeholder_FirstName = "{FN}";
    const string Placeholder_MiddleName = "{MN}";
    const string Placeholder_LastName = "{LN}";
    const string Placeholder_NameSuffix = "{NS}";
    const string Placeholder_MiddleInitials = "{MI}";

    readonly IMiddleInitialsParser _middleInitialsParser;

    public FullNameBuilder(IMiddleInitialsParser middleInitialsParser)
    {
        _middleInitialsParser = middleInitialsParser;
    }

    public string Build(string firstName, string middleName, string lastName, string nameSuffix, string format)
    {
        if (string.IsNullOrWhiteSpace(format))
            return "";

        var containsFirstName = format.Contains(Placeholder_FirstName);
        var containsMiddleName = format.Contains(Placeholder_MiddleName);
        var containsLastName = format.Contains(Placeholder_LastName);
        var containsNameSuffix = format.Contains(Placeholder_NameSuffix);
        var containsMiddleInitials = format.Contains(Placeholder_MiddleInitials);
        var builder = new StringBuilder(format);
        
        if (containsFirstName)
            builder.Replace(Placeholder_FirstName, firstName);

        if (containsMiddleName)
            builder.Replace(Placeholder_MiddleName, middleName);

        if (containsLastName)
            builder.Replace(Placeholder_LastName, lastName);

        if (containsMiddleInitials)
            builder.Replace(Placeholder_MiddleInitials, _middleInitialsParser.Parse(middleName));

        return builder.ToString();
    }
}