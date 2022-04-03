#nullable disable

namespace Nova.Identity.Entities;

public class UserLogin
{
    public long Id { get; set; }
    public int UserId { get; set; }
    public short TypeId { get; set; }
    public short StatusId { get; set; }
    public short Ordinal { get; set; }

    public bool IsDeleted { get; set; }
    public int? InsertedById { get; set; }
    public DateTimeOffset? InsertedOn { get; set; }
    public int? UpdatedById { get; set; }
    public DateTimeOffset? UpdatedOn { get; set; }
    public int? DeletedById { get; set; }
    public DateTimeOffset? DeletedOn { get; set; }

    public User User { get; set; }
    public UserLoginType Type { get; set; }
    public UserLoginStatus Status { get; set; }
}