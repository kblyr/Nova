using System.Net.Mime;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace Nova.Messaging;

public class ApiResult<TSuccessResponse, TApiSuccessResponse> : IResult where TSuccessResponse : Response
{
    readonly Response _response;
    readonly int _successStatusCode;

    public ApiResult(Response response, int successStatusCode)
    {
        _response = response;
        _successStatusCode = successStatusCode;
    }

    public async Task ExecuteAsync(HttpContext context)
    {
        context.Response.ContentType = MediaTypeNames.Application.Json;

        switch (_response)
        {
            case TSuccessResponse response:
                await RespondSuccess<TSuccessResponse, TApiSuccessResponse>(context, response, _successStatusCode);
                break;
            case FailedResponse response:
                await RespondFailed(context, response);
                break;
            default: throw new InvalidOperationException("Unsupported response type");
        }
    }

    static async Task RespondSuccess<TResponse, TApiResponse>(HttpContext context, TResponse response, int statusCode) where TResponse : Response
    {
        var mapper = context.RequestServices.GetRequiredService<ResponseMapper>();
        context.Response.StatusCode = statusCode;
        await context.Response.WriteAsJsonAsync(mapper.Map<TResponse, TApiResponse>(response));
    }

    static async Task RespondFailed(HttpContext context, FailedResponse response)
    {
        context.Response.StatusCode = StatusCodes.Status400BadRequest;
        await context.Response.WriteAsJsonAsync(new ApiFailedResponse(response.GetType().Name, response));
    }
}
