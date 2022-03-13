namespace Nova.Identity.Entities;

public class Permission
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Code { get; set; }
    public short? DomainId { get; set; }
    public short? ApplicationId { get; set; }
    public bool IsDeleted { get; set; }
    public int? InsertedById { get; set; }
    public DateTimeOffset? InsertedOn { get; set; }
    public int? UpdatedById { get; set; }
    public DateTimeOffset? UpdatedOn { get; set; }
    public int? DeletedById { get; set; }
    public DateTimeOffset? DeletedOn { get; set; }

    public Domain Domain { get; set; }
    public Application Application { get; set; }
    public IEnumerable<RolePermission> RolePermissions { get; set; }
    public IEnumerable<UserPermission> UserPermissions { get; set; }
}