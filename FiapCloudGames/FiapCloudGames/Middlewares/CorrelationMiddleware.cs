﻿using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Serilog.Context;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace FiapCloudGamesApi.Middlewares
{
    public class CorrelationMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<CorrelationMiddleware> _logger;
        private const string CorrelationIdHeader = "X-Correlation-ID";

        public CorrelationMiddleware(RequestDelegate next, ILogger<CorrelationMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var correlationId = GetOrCreateCorrelationId(context);
            AddCorrelationIdToResponse(context, correlationId);

            using (CreateLogScope(correlationId))
            {
                await LogAndExecuteRequestAsync(context, correlationId);
            }
        }

        private string GetOrCreateCorrelationId(HttpContext context)
        {
            if (!context.Request.Headers.TryGetValue(CorrelationIdHeader, out var correlationId))
            {
                correlationId = Guid.NewGuid().ToString();
            }

            return correlationId;
        }

        private void AddCorrelationIdToResponse(HttpContext context, string correlationId)
        {
            context.Response.OnStarting(() =>
            {
                if (!context.Response.Headers.ContainsKey(CorrelationIdHeader))
                {
                    context.Response.Headers.Add(CorrelationIdHeader, correlationId);
                }
                return Task.CompletedTask;
            });
        }

        private IDisposable CreateLogScope(string correlationId)
        {
            LogContext.PushProperty("CorrelationId", correlationId);
            return _logger.BeginScope("{CorrelationId}: {CorrelationIdValue}", correlationId, correlationId);
        }

        private async Task LogAndExecuteRequestAsync(HttpContext context, string correlationId)
        {
            var stopwatch = Stopwatch.StartNew();

            try
            {
                _logger.LogInformation("Request started: {Method} {Path}", context.Request.Method, context.Request.Path);

                await _next(context);

                stopwatch.Stop();
                _logger.LogInformation(
                    "Request finished: {Method} {Path} responded {StatusCode} in {ElapsedMilliseconds}ms",
                    context.Request.Method,
                    context.Request.Path,
                    context.Response.StatusCode,
                    stopwatch.ElapsedMilliseconds);
            }
            catch (Exception ex)
            {
                stopwatch.Stop();
                _logger.LogError(
                    ex,
                    "Request failed: {Method} {Path} after {ElapsedMilliseconds}ms",
                    context.Request.Method,
                    context.Request.Path,
                    stopwatch.ElapsedMilliseconds);
                throw;
            }
        }
    }
}
