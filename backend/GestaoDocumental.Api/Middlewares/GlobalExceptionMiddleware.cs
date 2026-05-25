using System.Net;
using System.Text.Json;
using FluentValidation;
using GestaoDocumental.Shared.Responses;

namespace GestaoDocumental.Api.Middlewares;

public class GlobalExceptionMiddleware
{
    private static readonly JsonSerializerOptions JsonOptions = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
    };

    private readonly RequestDelegate _next;
    private readonly ILogger<GlobalExceptionMiddleware> _logger;
    private readonly IHostEnvironment _environment;

    public GlobalExceptionMiddleware(
        RequestDelegate next,
        ILogger<GlobalExceptionMiddleware> logger,
        IHostEnvironment environment)
    {
        _next = next;
        _logger = logger;
        _environment = environment;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception exception)
        {
            await HandleExceptionAsync(context, exception);
        }
    }

    private async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        if (context.Response.HasStarted)
        {
            _logger.LogError(exception, "An unhandled exception occurred after the response started.");
            throw exception;
        }

        var (statusCode, message, errors) = MapException(exception);

        if (statusCode >= (int)HttpStatusCode.InternalServerError)
        {
            _logger.LogError(exception, "Unhandled exception: {Message}", exception.Message);
        }
        else
        {
            _logger.LogWarning(exception, "Handled exception: {Message}", exception.Message);
        }

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = statusCode;

        var response = ErrorResponse.Create(statusCode, message, errors);

        await context.Response.WriteAsync(JsonSerializer.Serialize(response, JsonOptions));
    }

    private (int StatusCode, string Message, IEnumerable<string>? Errors) MapException(Exception exception)
    {
        return exception switch
        {
            ValidationException validationException => (
                StatusCodes.Status400BadRequest,
                "Validation failed.",
                validationException.Errors.Select(error => error.ErrorMessage)),

            KeyNotFoundException keyNotFoundException => (
                StatusCodes.Status404NotFound,
                string.IsNullOrWhiteSpace(keyNotFoundException.Message)
                    ? "Resource not found."
                    : keyNotFoundException.Message,
                null),

            UnauthorizedAccessException unauthorizedAccessException => (
                StatusCodes.Status401Unauthorized,
                string.IsNullOrWhiteSpace(unauthorizedAccessException.Message)
                    ? "Unauthorized access."
                    : unauthorizedAccessException.Message,
                null),

            _ => (
                StatusCodes.Status500InternalServerError,
                _environment.IsDevelopment()
                    ? exception.Message
                    : "An unexpected error occurred.",
                _environment.IsDevelopment()
                    ? new[] { exception.ToString() }
                    : null)
        };
    }
}
