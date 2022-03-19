namespace Nova.HRIS.Entities;

public class Employee
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string MiddleName { get; set; }
    public string LastName { get; set; }
    public string NameSuffix { get; set; }
    public string MaidenMiddleName { get; set; }
    public bool? Sex { get; set; }
    public DateTime? BirthDate { get; set; }
    public string BirthPlace { get; set; }
    public string ContactNumber { get; set; }
    public short? CivilStatusId { get; set; }
    public short? NationalityId { get; set; }
    public string Address { get; set; }
    public short? BarangayId { get; set; }
    public short? CityId { get; set; }
    public short? ProvinceId { get; set; }
    public bool IsDeleted { get; set; }
    public int? InsertedById { get; set; }
    public DateTimeOffset? InsertedOn { get; set; }
    public int? UpdatedById { get; set; }
    public DateTimeOffset? UpdatedOn { get; set; }
    public int? DeletedById { get; set; }
    public DateTimeOffset? DeletedOn { get; set; }

    public CivilStatus CivilStatus { get; set; }
    public Nationality Nationality { get; set; }
    public Barangay Barangay { get; set; }
    public City City { get; set; }
    public Province Province { get; set; }
    
    public string FullName { get; set; }
    public string FullAddress { get; set; }
}