namespace Nova.HRIS.Entities;

public class City
{
    public short Id { get; set; }
    public string Name { get; set; }
    public short? ProvinceId { get; set; }
    public bool IsDeleted { get; set; }
    public int? InsertedById { get; set; }
    public DateTimeOffset? InsertedOn { get; set; }
    public int? UpdatedById { get; set; }
    public DateTimeOffset? UpdatedOn { get; set; }
    public int? DeletedById { get; set; }
    public DateTimeOffset? DeletedOn { get; set; }

    public Province Province { get; set; }
}