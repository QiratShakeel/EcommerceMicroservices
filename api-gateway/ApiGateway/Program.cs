using BuildingBlocks.Shared.Behaviors.Logging;
using Microsoft.AspNetCore.HttpOverrides;
using OpenIddict.Validation.AspNetCore;
using Serilog;
using System.Net;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog();

// -------------------- Logging --------------------
builder.Services.AddSharedLogging();

// -------------------- Authentication (OpenIddict Validation) --------------------
builder.Services.AddAuthentication(OpenIddictValidationAspNetCoreDefaults.AuthenticationScheme);

builder.Services.AddOpenIddict()
    .AddValidation(options =>
    {
        options.SetIssuer(new Uri(builder.Configuration["OpenIddict:Issuer"]!));

        options.AddAudiences("ecommerce_api");

        options.UseSystemNetHttp();

        options.UseAspNetCore();
    });

// -------------------- Authorization --------------------
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("authenticated", policy =>
    {
        policy.RequireAuthenticatedUser();
    });
});

// -------------------- YARP Reverse Proxy --------------------
builder.Services
    .AddReverseProxy()
    .LoadFromConfig(builder.Configuration.GetSection("ReverseProxy"))
    .ConfigureHttpClient((context, handler) =>
    {
        if (handler is SocketsHttpHandler socketsHandler)
        {
            socketsHandler.AllowAutoRedirect = false;

            // optional tuning (safe)
            socketsHandler.PooledConnectionLifetime = TimeSpan.FromMinutes(5);
        }
    });

// -------------------- CORS --------------------
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader());
});

var app = builder.Build();

// -------------------- Middleware --------------------
app.UseSerilogRequestLogging();

app.UseCors("AllowAll");

// 🔥 IMPORTANT FIX: buffer request body for form-data / x-www-form-urlencoded
app.Use(async (context, next) =>
{
    context.Request.EnableBuffering();
    await next();
});

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

// -------------------- Reverse Proxy --------------------
app.MapReverseProxy();

app.Run();