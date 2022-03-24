namespace Nova.Validation;

public interface IAccessValidationRule
{
    string Name { get; }
    IDictionary<string, object?> Data { get; }
}

public abstract class AccessValidationRuleBase
{
    IDictionary<string, object?>? _data;
    public IDictionary<string, object?> Data
    {
        get
        {
            if (_data is not null)
                return _data;

            _data = new Dictionary<string, object?>();
            SetData(_data);
            return _data;
        }
    }

    protected virtual void SetData(IDictionary<string, object?> data) { }
}

sealed class Role_AVR : AccessValidationRuleBase, IAccessValidationRule
{
    public string Name { get; } = "Nova.ValidateBy.Role";

    public int RoleId { get; }

    public Role_AVR(int roleId)
    {
        RoleId = roleId;
    }

    protected override void SetData(IDictionary<string, object?> data)
    {
        data.Add(nameof(RoleId), RoleId);
    }
}

sealed class Permission_AVR : AccessValidationRuleBase, IAccessValidationRule
{
    public string Name { get; } = "Nova.ValidateBy.Permission";

    public int PermissionId { get; }

    public Permission_AVR(int permissionId)
    {
        PermissionId = permissionId;
    }

    protected override void SetData(IDictionary<string, object?> data)
    {
        data.Add(nameof(PermissionId), PermissionId);
    }
}