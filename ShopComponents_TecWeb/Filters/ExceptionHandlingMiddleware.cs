using FluentValidation;
using ShopComponents.Core.CustomEntities;
using ShopComponents.Core.Exceptions;
using System.Text.Json;

namespace ShopComponents.Api.Middleware;

public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;

    public ExceptionHandlingMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (BusinessException ex)
        {
            await WriteErrorAsync(context, ex.StatusCode, "Business rule error", ex.Message, null);
        }
        catch (ValidationException ex)
        {
            var errors = ex.Errors.Select(e => new
            {
                field = e.PropertyName,
                error = e.ErrorMessage
            });

            await WriteErrorAsync(context, 400, "Validation error", "One or more validation errors occurred.", errors);
        }
        catch (Exception ex)
        {
            await WriteErrorAsync(context, 500, "Server error", ex.Message, null);
        }
    }

    private static async Task WriteErrorAsync(HttpContext context, int status, string title, string message, object? errors)
    {
        context.Response.StatusCode = status;
        context.Response.ContentType = "application/json";

        var response = new ErrorResponse
        {
            Status = status,
            Title = title,
            Message = message,
            Errors = errors,
            TraceId = context.TraceIdentifier
        };

        await context.Response.WriteAsync(JsonSerializer.Serialize(response));
    }
}