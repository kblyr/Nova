using Microsoft.AspNetCore.Http;

namespace Nova.Messaging;

public static class NovaResults
{
    public static IResult Mapped<TSuccessResponse, TApiSuccessResponse>(this IResultExtensions extensions, Response response, int statusCode) where TSuccessResponse : Response
    {
        return new ApiResult<TSuccessResponse, TApiSuccessResponse>(response, statusCode);
    }

    public static IResult MappedOK<TSuccessResponse, TApiSuccessResponse>(this IResultExtensions extensions, Response response) where TSuccessResponse : Response
    {
        return extensions.Mapped<TSuccessResponse, TApiSuccessResponse>(response, StatusCodes.Status200OK);
    }

    public static IResult MappedCreated<TSuccessResponse, TApiSuccessResponse>(this IResultExtensions extensions, Response response) where TSuccessResponse : Response
    {
        return extensions.Mapped<TSuccessResponse, TApiSuccessResponse>(response, StatusCodes.Status201Created);
    }
}