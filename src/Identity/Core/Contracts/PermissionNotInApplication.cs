namespace Nova.Identity.Contracts;

public record PermissionNotInApplication(int PermissionId, short? ApplicationId) : FailedResponse;