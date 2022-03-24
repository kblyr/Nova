namespace Nova.HRIS.Entities;

public class Employment
{
    public long Id { get; set; }
    public int EmployeeId { get; set; }
    public short TypeId { get; set; }
    public short OfficeId { get; set; }
    public int PositionId { get; set; }
    public DateTimeOffset EffectivityDateBegin { get; set; }
    public DateTimeOffset? EffectivityDateEnd { get; set; }
    public decimal? Salary { get; set; }
    public bool IsDeleted { get; set; }
    public int? InsertedById { get; set; }
    public DateTimeOffset? InsertedOn { get; set; }
    public int? UpdatedById { get; set; }
    public DateTimeOffset? UpdatedOn { get; set; }
    public int? DeletedById { get; set; }
    public DateTimeOffset? DeletedOn { get; set; }

    public Employee Employee { get; set; }
    public EmploymentType Type { get; set; }
    public Office Office { get; set; }
    public Position Position { get; set; }
}
