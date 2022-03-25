namespace Nova.Validation;

public interface ICurrentRoleIdsProvider
{
    IEnumerable<int> RoleIds { get; }
}
