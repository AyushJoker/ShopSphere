
using ShopSphere.ApiGateway.Configuration;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddReverseProxy()
                .LoadFromConfig(builder.Configuration.GetSection("ReverseProxy"));

// Configure API rate limiting policies
builder.Services.AddRateLimitingConfiguration();

var app = builder.Build();

app.UseRateLimiter();

app.MapReverseProxy()
    .RequireRateLimiting("GlobalPolicy");

app.Run();