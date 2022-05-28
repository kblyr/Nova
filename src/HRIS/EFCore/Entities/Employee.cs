#nullable disable
namespace Nova.HRIS.Entities;

public class Employee
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string MiddleName { get; set; }
    public string LastName { get; set; }
    public string NameSuffix { get; set; }
    public DateTime BirthDate { get; set; }
    public bool Sex { get; set; }
    public string ContactNumber { get; set; }
    public string EmailAddress { get; set; }
    public short? CivilStatusId { get; set; }
    public int? CitizenshipId { get; set; }
    public short EmploymentStatusId { get; set; }

    public string FullName { get; set; }

    public bool IsDeleted { get; set; }
    public int? InsertedById { get; set; }
    public DateTimeOffset? InsertedOn { get; set; }
    public int? UpdatedById { get; set; }
    public DateTimeOffset? UpdatedOn { get; set; }
    public int? DeletedById { get; set; }
    public DateTimeOffset? DeletedOn { get; set; }

    public CivilStatus CivilStatus { get; set; }
    public Citizenship Citizenship { get; set; }
    public EmploymentStatus EmploymentStatus { get; set; }
}