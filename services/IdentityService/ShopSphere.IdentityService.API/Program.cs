using Serilog;
using ShopSphere.IdentityService.API.Configurations;

var builder = WebApplication.CreateBuilder(args);

// Configure Serilog logging
builder.AddSerilogLogging();

// Register MVC controllers
builder.Services.AddControllers();

// Configure API rate limiting policies
builder.Services.AddRateLimitingConfiguration();

// Required for Swagger endpoint discovery
builder.Services.AddEndpointsApiExplorer();

// Configure Swagger/OpenAPI documentation
builder.Services.AddSwaggerDocumentation();

// Register application services and repositories
builder.Services.AddApplicationServices(builder.Configuration);

// Configure Redis distributed caching
builder.Services.AddRedisCache(builder.Configuration);

// Configure JWT authentication
builder.Services.AddJwtAuthentication(builder.Configuration);

var app = builder.Build();

// Enable Swagger only in development
//if (app.Environment.IsDevelopment())
//{
    app.UseSwagger();

    app.UseSwaggerUI();
//}

// Redirect HTTP requests to HTTPS
app.UseHttpsRedirection();

// Log all incoming HTTP requests
app.UseSerilogRequestLogging();

// Global exception handling middleware
app.UseGlobalExceptionMiddleware();

// Apply API rate limiting middleware
app.UseRateLimiter();

// Validate JWT tokens and authenticate users
app.UseAuthentication();

// Check user authorization and roles
app.UseAuthorization();

// Map controller endpoints
app.MapControllers();

app.Run();