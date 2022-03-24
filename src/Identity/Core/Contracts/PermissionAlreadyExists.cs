namespace Nova.Identity.Contracts;

public record PermissionAlreadyExists(string Name, short? DomainId, short? ApplicationId) : FailedResponse;