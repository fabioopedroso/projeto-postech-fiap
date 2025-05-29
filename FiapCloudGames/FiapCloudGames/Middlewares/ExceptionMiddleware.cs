﻿using Application.Exceptions;
using System.Net;
using System.Text.Json;

namespace API.Middlewares;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionMiddleware> _logger;

    public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
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
            LogException(ex);
            await WriteProblemResponseAsync(context, ex);
        }
    }

    private void LogException(Exception ex)
    {
        _logger.LogError(ex, "Unhandled exception occurred | Message: {Message}", ex.Message);
    }

    private async Task WriteProblemResponseAsync(HttpContext context, Exception exception)
    {
        context.Response.ContentType = "application/problem+json";
        context.Response.StatusCode = GetStatusCode(exception);

        var problemDetails = BuildProblemDetails(context, exception, context.Response.StatusCode);

        var json = JsonSerializer.Serialize(problemDetails);
        await context.Response.WriteAsync(json);
    }

    private int GetStatusCode(Exception exception) => exception switch
    {
        NotFoundException => StatusCodes.Status404NotFound,
        ConflictException => StatusCodes.Status409Conflict,
        ValidationException => StatusCodes.Status400BadRequest,
        BusinessException => StatusCodes.Status400BadRequest,
        UnauthorizedException => StatusCodes.Status401Unauthorized,
        _ => StatusCodes.Status500InternalServerError
    };

    private object BuildProblemDetails(HttpContext context, Exception exception, int statusCode) => new
    {
        type = $"https://httpstatuses.com/{statusCode}",
        title = GetTitleForStatusCode(statusCode),
        status = statusCode,
        detail = exception.Message,
        instance = context.Request.Path
    };

    private string GetTitleForStatusCode(int statusCode) => statusCode switch
    {
        StatusCodes.Status400BadRequest => "Bad Request",
        StatusCodes.Status401Unauthorized => "Unauthorized",
        StatusCodes.Status404NotFound => "Not Found",
        StatusCodes.Status409Conflict => "Conflict",
        StatusCodes.Status500InternalServerError => "Internal Server Error",
        _ => "An error occurred"
    };
}
