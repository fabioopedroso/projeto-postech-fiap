using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System.Text.Json;
using System.Threading.Tasks;

public static class StatusCodePagesExtensions
{
    public static IApplicationBuilder UseCustomStatusCodePages(this IApplicationBuilder app)
    {
        app.UseStatusCodePages(async context =>
        {
            var response = context.HttpContext.Response;
            var statusCode = response.StatusCode;

            if (statusCode == StatusCodes.Status401Unauthorized || statusCode == StatusCodes.Status403Forbidden)
            {
                response.ContentType = "application/problem+json";

                var problemDetails = new
                {
                    type = statusCode == 401 ? "https://httpstatuses.com/401" : "https://httpstatuses.com/403",
                    title = statusCode == 401 ? "Unauthorized" : "Forbidden",
                    status = statusCode,
                    detail = statusCode == 401
                        ? "Authentication is required to access this resource."
                        : "You do not have permission to access this resource.",
                    instance = context.HttpContext.Request.Path
                };

                await response.WriteAsync(JsonSerializer.Serialize(problemDetails));
            }
        });

        return app;
    }
}
