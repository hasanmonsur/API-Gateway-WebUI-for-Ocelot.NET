using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Add Ocelot without Consul
builder.Services.AddOcelot();

// Load Ocelot configuration from ocelot.json
builder.Configuration.AddJsonFile("ocelot.json", optional: false, reloadOnChange: true);

// Add MVC and Swagger
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors("AllowAll");

// Use Ocelot
// Use Ocelot, but skip for specific routes
app.Use(async (context, next) =>
{
    if (context.Request.Path.StartsWithSegments("/api/config"))
    {
        // Skip Ocelot for /api/config routes
        await next();
    }
    else
    {
        // Use Ocelot for all other routes
        await app.UseOcelot();
    }
});

app.MapControllers(); // Map your controllers

app.Run();