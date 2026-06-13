using System.Text.Json;
using ShopSphere.PaymentService.API.Middleware.Models;
using ShopSphere.PaymentService.Application.Common.Exceptions;

namespace ShopSphere.PaymentService.API.Middleware;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionMiddleware> _logger;

    public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
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
            _logger.LogError(ex,"Unhandled exception occurred");

            await HandleExceptionAsync(
                context,
                ex);
        }
    }

    private static async Task HandleExceptionAsync(
        HttpContext context,
        Exception exception)
    {
        var statusCode = exception switch
        {
            BadRequestException =>
                StatusCodes.Status400BadRequest,

            NotFoundException =>
                StatusCodes.Status404NotFound,

            _ =>
                StatusCodes.Status500InternalServerError
        };

        var title = statusCode switch
        {
            StatusCodes.Status400BadRequest =>
                "Bad Request",

            StatusCodes.Status404NotFound =>
                "Resource Not Found",

            _ =>
                "Internal Server Error"
        };

        var response = new ErrorResponse
        {
            Status = statusCode,
            Title = title,
            Detail = exception.Message,
            TraceId = context.TraceIdentifier
        };

        context.Response.ContentType ="application/json";

        context.Response.StatusCode = statusCode;

        await context.Response.WriteAsync(
            JsonSerializer.Serialize(response));
    }
}