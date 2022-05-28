#nullable disable
namespace Nova.HRIS.Entities;

public class EmployeeAddress
{
    public long Id { get; set; }
    public int EmployeeId { get; set; }
    public short TypeId { get; set; }
    public string HouseBlockLotNumber { get; set; }
    public string Street { get; set; }
    public string SubdivisionVillage { get; set; }
    public int? BarangayId { get; set; }
    public int? CityId { get; set; }
    public int? ProvinceId { get; set; }

    public string FullName { get; set; }

    public bool IsDeleted { get; set; }
    public int? InsertedById { get; set; }
    public DateTimeOffset? InsertedOn { get; set; }
    public int? UpdatedById { get; set; }
    public DateTimeOffset? UpdatedOn { get; set; }
    public int? DeletedById { get; set; }
    public DateTimeOffset? DeletedOn { get; set; }

    public Employee Employee { get; set; }
    public AddressType Type { get; set; }
    public Barangay Barangay { get; set; }
    public City City { get; set; }
    public Province Province { get; set; }
}