namespace Nova.Identity.Contracts;

public record PermissionNotInDomain(int PermissionId, short? DomainId) : FailedResponse;