using Microsoft.AspNetCore.Http;

namespace Nova.Messaging;

public static class ResponseTypeRegistry_Extensions
{
    public static ResponseTypeRegistry RegisterOK<TResponse>(this ResponseTypeRegistry registry) where TResponse : Response
    {
        return registry.Register<TResponse>(StatusCodes.Status200OK);
    }

    public static ResponseTypeRegistry RegisterOK<TResponse, TApiResponse>(this ResponseTypeRegistry registry) where TResponse : Response
    {
        return registry.Register<TResponse, TApiResponse>(StatusCodes.Status200OK);
    }

    public static ResponseTypeRegistry RegisterCreated<TResponse>(this ResponseTypeRegistry registry) where TResponse : Response
    {
        return registry.Register<TResponse>(StatusCodes.Status201Created);
    }

    public static ResponseTypeRegistry RegisterCreated<TResponse, TApiResponse>(this ResponseTypeRegistry registry) where TResponse : Response
    {
        return registry.Register<TResponse, TApiResponse>(StatusCodes.Status201Created);
    }

    public static ResponseTypeRegistry RegisterBadRequest<TResponse>(this ResponseTypeRegistry registry) where TResponse : Response
    {
        return registry.Register<TResponse>(StatusCodes.Status400BadRequest);
    }

    public static ResponseTypeRegistry RegisterBadRequest<TResponse, TApiResponse>(this ResponseTypeRegistry registry) where TResponse : Response
    {
        return registry.Register<TResponse, TApiResponse>(StatusCodes.Status400BadRequest);
    }

    public static ResponseTypeRegistry RegisterNotFound<TResponse>(this ResponseTypeRegistry registry) where TResponse : Response
    {
        return registry.Register<TResponse>(StatusCodes.Status404NotFound);
    }

    public static ResponseTypeRegistry RegisterNotFound<TResponse, TApiResponse>(this ResponseTypeRegistry registry) where TResponse : Response
    {
        return registry.Register<TResponse, TApiResponse>(StatusCodes.Status404NotFound);
    }

    public static ResponseTypeRegistry RegisterConflict<TResponse>(this ResponseTypeRegistry registry) where TResponse : Response
    {
        return registry.Register<TResponse>(StatusCodes.Status409Conflict);
    }

    public static ResponseTypeRegistry RegisterConflict<TRespnse, TApiResponse>(this ResponseTypeRegistry registry) where TRespnse : Response
    {
        return registry.Register<TRespnse, TApiResponse>(StatusCodes.Status409Conflict);
    }
}