namespace Nova.Identity.Contracts;

public record ApplicationNotInDomain(short ApplicationId, short? DomainId) : FailedResponse;