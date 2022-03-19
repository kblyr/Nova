namespace Nova.Identity.Contracts;

public record SavePermissionsOfRole : Request
{
    public int RoleId { get; init; }
    public IEnumerable<int> AddedIds { get; init; } = Enumerable.Empty<int>();
    public IEnumerable<int> RemovedIds { get; init; } = Enumerable.Empty<int>();

    public record Response(IEnumerable<int> AddedIds, IEnumerable<int> RemovedIds) : Messaging.Response;
}