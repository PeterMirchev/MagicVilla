using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace MagicVillaAPI.Filters;

public class GlobalExceptionFilter : IExceptionFilter
{
    private readonly ILogger<GlobalExceptionFilter> _logger;

    public GlobalExceptionFilter(ILogger<GlobalExceptionFilter> logger)
    {
        _logger = logger;
    }

    public void OnException(ExceptionContext context)
    {
        int statusCode;
        string message = context.Exception.Message;

        // Customize response by exception type
        switch (context.Exception)
        {
            case KeyNotFoundException:
                statusCode = (int)HttpStatusCode.NotFound;
                break;

            case ArgumentException:
                statusCode = (int)HttpStatusCode.BadRequest;
                break;

            default:
                statusCode = (int)HttpStatusCode.InternalServerError;
                break;
        }

        _logger.LogError(context.Exception, "Unhandled exception occurred: {Message}", context.Exception.Message);
        var errorResponse = new
        {
            StatusCode = statusCode,
            Message = message,
            Errors = new[] { message },
            Timestamp = DateTimeOffset.UtcNow
        };

        context.Result = new JsonResult(errorResponse)
        {
            StatusCode = statusCode
        };

        context.ExceptionHandled = true;
    }
}