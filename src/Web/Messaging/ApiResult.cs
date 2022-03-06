using System.Net.Mime;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace Nova.Messaging;

public class ApiResult : IResult
{
    readonly Response _response;

    public ApiResult(Response response)
    {
        _response = response;
    }

    public async Task ExecuteAsync(HttpContext context)
    {
        var mapper = context.RequestServices.GetRequiredService<ResponseMapper>();
        var (data, statusCode) = mapper.Map(_response);

        context.Response.ContentType = MediaTypeNames.Application.Json;
        context.Response.StatusCode = statusCode;
        await context.Response.WriteAsJsonAsync(data);
    }
}
