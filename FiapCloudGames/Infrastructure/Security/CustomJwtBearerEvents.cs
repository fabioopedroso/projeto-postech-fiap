using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;

namespace FiapCloudGamesApi.Security
{
    public class CustomJwtBearerEvents : JwtBearerEvents
    {
        public override Task Challenge(JwtBearerChallengeContext context)
        {
            if (!context.Response.HasStarted)
            {
                context.HandleResponse();

                context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                context.Response.ContentType = "application/json";

                var response = new
                {
                    error = "Unauthorized",
                    message = "Token de autenticação inválido ou ausente."
                };

                var json = JsonSerializer.Serialize(response);
                return context.Response.WriteAsync(json);
            }

            return Task.CompletedTask;
        }

        public override Task AuthenticationFailed(AuthenticationFailedContext context)
        {
            if (!context.Response.HasStarted)
            {
                context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                context.Response.ContentType = "application/json";

                var response = new
                {
                    error = "Authentication Failed",
                    message = "Falha na autenticação do token.",
                    detail = context.Exception.Message
                };

                var json = JsonSerializer.Serialize(response);
                return context.Response.WriteAsync(json);
            }

            return Task.CompletedTask;
        }

        public override Task Forbidden(ForbiddenContext context)
        {
            if (!context.Response.HasStarted)
            {
                context.Response.StatusCode = (int)HttpStatusCode.Forbidden;
                context.Response.ContentType = "application/json";

                var response = new
                {
                    error = "Forbidden",
                    message = "Você não tem permissão para acessar este recurso."
                };

                var json = JsonSerializer.Serialize(response);
                return context.Response.WriteAsync(json);
            }

            return Task.CompletedTask;
        }
    }
}
