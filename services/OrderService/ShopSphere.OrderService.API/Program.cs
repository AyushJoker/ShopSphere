using Serilog;
using ShopSphere.OrderService.API.Configuration;
using ShopSphere.OrderService.API.Configurations;
using ShopSphere.OrderService.Application.DependencyInjection;
using ShopSphere.OrderService.Infrastructure.DependencyInjection;


var builder = WebApplication.CreateBuilder(args);

// Configure Serilog logging
builder.AddSerilogLogging();

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerDocumentation();

builder.Services.AddApplication();

builder.Services.AddInfrastructure(builder.Configuration);

builder.Services.AddHttpContextAccessor();

var app = builder.Build();

app.UseSwagger();

app.UseSwaggerUI();

app.UseHttpsRedirection();
// Log all incoming HTTP requests
app.UseSerilogRequestLogging();


app.UseGlobalExceptionMiddleware();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();