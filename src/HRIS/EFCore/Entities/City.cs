#nullable disable
namespace Nova.HRIS.Entities;

public record City
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int? ProvinceId { get; set; }

    public Province Province { get; set; }
    public IEnumerable<Barangay> Barangays { get; set; }

    public bool IsDeleted { get; set; }
    public int? InsertedById { get; set; }
    public DateTimeOffset? InsertedOn { get; set; }
    public int? UpdatedById { get; set; }
    public DateTimeOffset? UpdatedOn { get; set; }
    public int? DeletedById { get; set; }
    public DateTimeOffset? DeletedOn { get; set; }
}