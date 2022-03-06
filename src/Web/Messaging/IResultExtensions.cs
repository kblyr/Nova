using Microsoft.AspNetCore.Http;

namespace Nova.Messaging;

public static class NovaResults
{
    public static IResult Mapped(this IResultExtensions extensions, Response response)
    {
        return new ApiResult(response);
    }
}