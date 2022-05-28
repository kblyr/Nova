namespace Nova.Utilities;

public interface IAddressFullNameBuilder
{
    string Build(
        string unitRoomNumber = "", 
        string houseNumber = "", 
        string building = "", 
        string blockNumber = "", 
        string lotNumber = "", 
        string phaseNumber = "", 
        string street = "", 
        string subdivisionVillage = "", 
        string barangay = "", 
        string city = "", 
        string province = "", 
        string zipCode = "");
}