using ShopComponents.Core.CustomEntities;
using ShopComponents.Core.Exceptions;
using System.Net;
using System.Text.Json;

namespace ShopComponents.Api.Middleware;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;

    public ExceptionMiddleware(RequestDelegate next)
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
            await HandleExceptionAsync(context, ex.StatusCode, ex.Message);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, 500, "Error inesperado: " + ex.Message);
        }
    }

    private static Task HandleExceptionAsync(HttpContext context, int statusCode, string message)
    {
        var response = new ErrorResponse
        {
            Status = statusCode,
            Message = message
        };

        var json = JsonSerializer.Serialize(response);
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = statusCode;
        return context.Response.WriteAsync(json);
    }
}