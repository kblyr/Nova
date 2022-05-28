#nullable disable
namespace Nova.HRIS.Utilities;

public interface IAddressFullNameSource
{
    string UnitRoomNumber { get; }
    string HouseNumber { get; }
}