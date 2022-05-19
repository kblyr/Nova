namespace Nova;

public static class ResponseTypeMapRegistryExtensions
{
    public static IResponseTypeMapRegistry Register<TResponse, TApiResponse>(this IResponseTypeMapRegistry registry, int statusCode) 
        where TResponse : IResponse
        where TApiResponse : IApiResponse
    {
        return registry.Register(typeof(TResponse), typeof(TApiResponse), statusCode);
    }

    public static IResponseTypeMapRegistry RegisterOK<TResponse, TApiResponse>(this IResponseTypeMapRegistry registry) 
        where TResponse : IResponse
        where TApiResponse : IApiResponse
    {
        return registry.Register<TResponse, TApiResponse>(StatusCodes.Status200OK);
    }

    public static IResponseTypeMapRegistry RegisterCreated<TResponse, TApiResponse>(this IResponseTypeMapRegistry registry) 
        where TResponse : IResponse
        where TApiResponse : IApiResponse
    {
        return registry.Register<TResponse, TApiResponse>(StatusCodes.Status201Created);
    }

    public static IResponseTypeMapRegistry RegisterBadRequest<TResponse, TApiResponse>(this IResponseTypeMapRegistry registry) 
        where TResponse : IResponse
        where TApiResponse : IApiResponse
    {
        return registry.Register<TResponse, TApiResponse>(StatusCodes.Status400BadRequest);
    }

    public static IResponseTypeMapRegistry RegisterNotFound<TResponse, TApiResponse>(this IResponseTypeMapRegistry registry) 
        where TResponse : IResponse
        where TApiResponse : IApiResponse
    {
        return registry.Register<TResponse, TApiResponse>(StatusCodes.Status404NotFound);
    }
}