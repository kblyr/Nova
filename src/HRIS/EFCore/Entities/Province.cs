#nullable disable
namespace Nova.HRIS.Entities;

public record Province
{
    public int Id { get; set; }
    public string Name { get; set; }

    public IEnumerable<City> Cities { get; set; }

    public bool IsDeleted { get; set; }
    public int? InsertedById { get; set; }
    public DateTimeOffset? InsertedOn { get; set; }
    public int? UpdatedById { get; set; }
    public DateTimeOffset? UpdatedOn { get; set; }
    public int? DeletedById { get; set; }
    public DateTimeOffset? DeletedOn { get; set; }
}