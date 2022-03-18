namespace Nova.Identity.Contracts;

public record RoleAlreadyExists(string Name, short? DomainId, short? ApplicationId) : FailedResponse;