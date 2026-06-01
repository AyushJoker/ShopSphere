namespace ShopSphere.IdentityService.API.Middleware.Models;

public sealed class ErrorResponse
{
    public int Status { get; set; }

    public string Title { get; set; } = string.Empty;

    public string Detail { get; set; } = string.Empty;

    public string? TraceId { get; set; }
}