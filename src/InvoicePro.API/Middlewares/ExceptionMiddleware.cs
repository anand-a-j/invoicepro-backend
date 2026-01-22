using System.Net;
using System.Text.Json;
using InvoicePro.API.Core;
using InvoicePro.Application.Exceptions;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionMiddleware> _logger;
    public ExceptionMiddleware(
        RequestDelegate next,
        ILogger<ExceptionMiddleware> logger
    )
    {
        _next = next;
        _logger = logger;
    }
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unhandled exception occurred");
            await HandleExceptionAsync(context, ex);
        }
    }
    private static async Task HandleExceptionAsync(
        HttpContext context,
        Exception exception
    )
    {
        context.Response.ContentType = "application/json";
        HttpStatusCode statusCode = HttpStatusCode.InternalServerError;
        string message = "Something went wrong. Please try again later.";
        object? errors = null;
        if (exception is AppException appException)
        {
            statusCode = appException.StatusCode;
            message = appException.Message;
        }
        context.Response.StatusCode = (int)statusCode;
        var response = ApiResponse<object>.Fail(
          message: message,
          errors: errors
      );
        var json = JsonSerializer.Serialize(response,
        new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        }
        );
        await context.Response.WriteAsync(json);
    }
}
