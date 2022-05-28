#nullable disable
namespace Nova.HRIS.Entities;

public class EmployeeSalaryGradeStep
{
    public long Id { get; set; }
    public int EmployeeId { get; set; }
    public short Grade { get; set; }
    public short Step { get; set; }
    public DateTimeOffset EffectivityBeginDate { get; set; }
    public DateTimeOffset? EffectivityEndDate { get; set; }

    public Employee Employee { get; set; }

    public bool IsDeleted { get; set; }
    public int? InsertedById { get; set; }
    public DateTimeOffset? InsertedOn { get; set; }
    public int? DeletedById { get; set; }
    public DateTimeOffset? DeletedOn { get; set; }
}