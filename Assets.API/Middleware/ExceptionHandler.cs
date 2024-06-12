using Microsoft.AspNetCore.Diagnostics;
using System.Net;

namespace Assets.API.Middleware;

public class ExceptionHandler
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionHandler> _logger;

    public ExceptionHandler(RequestDelegate next, ILogger<ExceptionHandler> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext httpContext)
    {
        if (httpContext.Response.StatusCode == (int)HttpStatusCode.OK)
        {
            await _next(httpContext);
        }
        else
        {
            IExceptionHandlerFeature? feature = httpContext.Features.Get<IExceptionHandlerFeature>();
            httpContext.Response.ContentType = "application/json";
            _logger.LogError(feature?.Error?.StackTrace ?? "Unknow Error has Occured");
            await httpContext.Response.WriteAsync(feature?.Error?.Message ?? "Unknow Error has Occured");
        }
    }
}