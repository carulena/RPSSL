using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace RpsslGameApi.Application.Filters;

public class GlobalExceptionFilter : IExceptionFilter
{
    private readonly ILogger<GlobalExceptionFilter> _logger;

    public GlobalExceptionFilter(ILogger<GlobalExceptionFilter> logger)
    {
        _logger = logger;
    }

    public void OnException(ExceptionContext context)
    {
        var (statusCode, message) = context.Exception switch
        {
            KeyNotFoundException e => (StatusCodes.Status404NotFound, e.Message),
            ArgumentException e => (StatusCodes.Status400BadRequest, e.Message),
            _ => (StatusCodes.Status500InternalServerError, "An unexpected error occurred.")
        };

        _logger.LogError(context.Exception, "Unhandled exception: {Message}", context.Exception.Message);

        context.Result = new ObjectResult(new { error = message })
        {
            StatusCode = statusCode
        };

        context.ExceptionHandled = true;
    }
}