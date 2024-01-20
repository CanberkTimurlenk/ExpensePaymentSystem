using System.Net;
using System.Text.Json;
using Serilog;

namespace FinalCase.Api.Middlewares;

public class ErrorHandlerMiddleware(RequestDelegate next)
{
    private readonly RequestDelegate next = next;

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await next.Invoke(context);
        }
        catch (Exception exception)
        {
            Log.Error(exception, "UnexpectedError");
            Log.Fatal(
                $"Path={context.Request.Path} || " +
                $"Method={context.Request.Method} || " +
                $"Exception={exception.Message}"
                
            );

            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            context.Response.ContentType = "application/json";
            await context.Response.WriteAsync(JsonSerializer.Serialize("Internal error!"));
        }
    }
}
