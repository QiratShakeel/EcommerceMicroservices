using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Json; // .NET 7+
using System.Text.Json;
using Serilog;
using Microsoft.Extensions.Logging;
using FluentValidation;

namespace BuildingBlocks.Shared.Exceptions
{
    public class GlobalExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<GlobalExceptionMiddleware> _logger;

        public GlobalExceptionMiddleware(RequestDelegate next, ILogger<GlobalExceptionMiddleware> logger)
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
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        public async Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            context.Response.ContentType = "application/json";

            var statusCode = ex switch
            {
                DomainException => StatusCodes.Status400BadRequest,
                ValidationException => StatusCodes.Status422UnprocessableEntity,
                NotFoundException => StatusCodes.Status404NotFound,
                _ => StatusCodes.Status500InternalServerError
            };

            context.Response.StatusCode = statusCode;
            _logger.LogError(ex, "Unhandled Exception occurred");

            var response = new
            {
                error = ex.Message,
                status = statusCode,
                traceId = context.TraceIdentifier
            };

            // ? Use WriteAsJsonAsync instead of WriteJsonAsync
            await context.Response.WriteAsJsonAsync(response);

            // If using older .NET version (<7), use custom helper:
            // await context.Response.WriteAsync(JsonSerializer.Serialize(response));
        }
    }
}
