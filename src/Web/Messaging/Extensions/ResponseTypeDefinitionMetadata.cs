namespace Nova.Messaging;

public static class ResponseTypeDefinitionMetadata_Extensions
{
    const string CreateRoute = "CreateRoute";

    public static ResponseTypeDefinitionMetadata WithCreateRoute(this ResponseTypeDefinitionMetadata metadata, Func<string, object> createRoute)
    {
        metadata[CreateRoute] = createRoute;
        return metadata;
    }

    public static Func<string, object>? GetCreateRoute(this ResponseTypeDefinitionMetadata metadata)
    {
        var value = metadata[CreateRoute];

        if (value is Func<string, object> createRoute)
            return createRoute;
        
        return null;
    }
}