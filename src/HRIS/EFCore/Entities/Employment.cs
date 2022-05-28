#nullable disable
namespace Nova.HRIS.Entities;

public record Employment
{
    public long Id { get; set; }
    public int EmployeeId { get; set; }
    public short TypeId { get; set; }
    public int OfficeId { get; set; }
    public int PositionId { get; set; }
    public decimal? Salary { get; set; }
    public DateTimeOffset EffectivityBeginDate { get; set; }
    public DateTimeOffset? EffectivityEndDate { get; set; }

    public Employee Employee { get; set; }
    public EmploymentType EmploymentType { get; set; }
    public Office Office { get; set; }
    public Position Position { get; set; }

    public bool IsDeleted { get; set; }
    public int? InsertedById { get; set; }
    public DateTimeOffset? InsertedOn { get; set; }
    public int? DeletedById { get; set; }
    public DateTimeOffset? DeletedOn { get; set; }
}