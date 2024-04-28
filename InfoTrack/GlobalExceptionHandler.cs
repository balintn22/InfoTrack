using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Net;
using System.Threading.Tasks;

namespace SeoStat.UI.Mvc;

/// <summary>
/// Abstract handler for all exceptions.
/// </summary>
public class GlobalExceptionHandler
{
    private readonly RequestDelegate _next;
    private readonly ILogger<GlobalExceptionHandler> _logger;

    public GlobalExceptionHandler(
        RequestDelegate next,
        ILogger<GlobalExceptionHandler> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        // TODO: Implement specific exception handlers as needed
        catch (Exception exception)
        {
            // log the error
            _logger.LogError(exception, "error during executing {Context}", context.Request.Path.Value);
            var response = context.Response;
            response.ContentType = "application/json";

            response.StatusCode = (int)HttpStatusCode.InternalServerError;
            await response.WriteAsync("Server Error");
        }
    }
}