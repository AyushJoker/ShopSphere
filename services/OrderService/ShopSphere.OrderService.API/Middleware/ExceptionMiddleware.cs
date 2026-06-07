namespace ShopSphere.OrderService.API.Middleware;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionMiddleware> _logger;

    public ExceptionMiddleware(
        RequestDelegate next,
        ILogger<ExceptionMiddleware> logger)
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
        catch (ProductNotFoundException ex)
        {
            context.Response.StatusCode = StatusCodes.Status404NotFound;

            await context.Response.WriteAsJsonAsync(
                     new ErrorResponseDto
                     {
                         StatusCode = 404,
                         Message = ex.Message
                     });
        }
        catch (OrderNotFoundException ex)
        {
            context.Response.StatusCode = StatusCodes.Status404NotFound;

            await context.Response.WriteAsJsonAsync(
                     new ErrorResponseDto
                     {
                         StatusCode = 404,
                         Message = ex.Message
                     });
        }
        catch (ProductServiceUnavailableException ex)
        {
            context.Response.StatusCode =
                StatusCodes.Status503ServiceUnavailable;

            await context.Response.WriteAsJsonAsync(
                 new ErrorResponseDto
                 {
                     StatusCode = 503,
                     Message = ex.Message
                 });
        }
        catch (Exception ex)
        {
            _logger.LogError(
                ex,
                "Unhandled exception occurred");

            context.Response.StatusCode =
                StatusCodes.Status500InternalServerError;

            await context.Response.WriteAsJsonAsync(
                 new ErrorResponseDto
                 {
                     StatusCode = 500,
                     Message = ex.Message
                 });
        }
    }
}