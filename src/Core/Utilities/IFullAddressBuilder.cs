using System.Text;
namespace Nova.Utilities;

public interface IFullAddressBuilder
{
    string Build(params string[] addressItem);
}

sealed class FullAddressBuilder : IFullAddressBuilder
{
    public string Build(params string[] addressItems)
    {
        return string.Join(", ", addressItems
            .Where(item => !string.IsNullOrWhiteSpace(item))
            .Select(item => item.Trim()));
    }
}
