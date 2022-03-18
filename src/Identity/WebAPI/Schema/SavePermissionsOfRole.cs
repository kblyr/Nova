namespace Nova.Identity.Schema;

public static class SavePermissionsOfRole
{
    public record Request(IEnumerable<int> AddedIds, IEnumerable<int> RemovedIds);

    public record Response(IEnumerable<int> AddedIds, IEnumerable<int> RemovedIds);
}