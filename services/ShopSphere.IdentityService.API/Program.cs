using Serilog;
using ShopSphere.IdentityService.API.Configurations;

var builder = WebApplication.CreateBuilder(args);

builder.AddSerilogLogging();

// Add services to the container.
builder.Services.AddControllers();

// Swagger / OpenAPI
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerDocumentation();

builder.Services.AddApplicationServices(builder.Configuration);

builder.Services.AddRedisCache(builder.Configuration);

builder.Services.AddJwtAuthentication(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseSerilogRequestLogging();

app.UseHttpsRedirection();

app.UseGlobalExceptionMiddleware();

app.UseAuthentication();

app.UseAuthorization();
app.MapControllers();

app.Run();