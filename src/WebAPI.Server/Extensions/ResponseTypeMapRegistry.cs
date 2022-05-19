namespace Nova;

public static class ResponseTypeMapRegistryExtensions
{
    public static IResponseTypeMapRegistry Register<TResponse, TApiResponse>(this IResponseTypeMapRegistry registry, int statusCode) 
        where TResponse : IResponse
        where TApiResponse : IApiResponse
    {
        return registry.Register(typeof(TResponse), typeof(TApiResponse), statusCode);
    }
}