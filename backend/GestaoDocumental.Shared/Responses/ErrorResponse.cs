namespace GestaoDocumental.Shared.Responses;

public class ErrorResponse
{
    public bool Success { get; init; } = false;

    public string Message { get; init; } = string.Empty;

    public IEnumerable<string>? Errors { get; init; }

    public int StatusCode { get; init; }

    public static ErrorResponse Create(
        int statusCode,
        string message,
        IEnumerable<string>? errors = null) =>
        new()
        {
            StatusCode = statusCode,
            Message = message,
            Errors = errors
        };
}
