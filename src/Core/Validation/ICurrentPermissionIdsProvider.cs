namespace Nova.Validation;

public interface ICurrentPermissionIdsProvider
{
    IEnumerable<int> PermissionIds { get; }
}