#nullable disable
namespace Nova.Utilities;

public interface IFullNameBuilder
{
    string Build(string firstName, string middleName, string lastName, string nameSuffix);
}
