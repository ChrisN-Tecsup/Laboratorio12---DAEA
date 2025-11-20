using Hangfire;
using Hangfire.MemoryStorage; 
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddHangfire(config => config.UseMemoryStorage());
builder.Services.AddHangfireServer();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Lab12 - Hangfire API ChristianMamani",
        Version = "v1"
    });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Lab12 - Hangfire API v1");
    });
}

app.UseHangfireDashboard("/hangfire");

app.MapControllers();

RecurringJob.AddOrUpdate(
    "job-limpieza-datos",
    () => new DataCleanupService().CleanOldData(),
    Cron.Daily); 

app.Run();