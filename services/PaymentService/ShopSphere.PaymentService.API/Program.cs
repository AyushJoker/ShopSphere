using Serilog;
using ShopSphere.PaymentService.API.Configuration;
using ShopSphere.PaymentService.API.Configurations;
using ShopSphere.PaymentService.Application.DependencyInjection;
using ShopSphere.PaymentService.Infrastructure.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// Configure Serilog logging
builder.AddSerilogLogging();

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerDocumentation();

builder.Services.AddApplication();

builder.Services.AddInfrastructure(builder.Configuration);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();

    app.UseSwaggerUI();
}
// Log all incoming HTTP requests
app.UseSerilogRequestLogging();

app.UseGlobalExceptionMiddleware();

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();