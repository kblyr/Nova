namespace Nova.Converters;

static class MapConverters
{
    public static UserIdConverter UserId => MapContext.Current.GetService<UserIdConverter>();
}