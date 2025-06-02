using API.Middlewares;
using FiapCloudGamesApi.Middlewares;
using FiapCloudGamesApi.Security;
using FiapCloudGamesApi.Swagger;
using Infrastructure.IoC;
using Infrastructure.Persistense;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog;
using Serilog.Enrichers;
using Serilog.Events;
using System.Text;

Log.Logger = new LoggerConfiguration()
    .Enrich.FromLogContext()
    .Enrich.WithMachineName()
    .Enrich.WithThreadId()
    .Enrich.WithProperty("Application", "FiapCloudGamesApi")
    .MinimumLevel.Information()
    .WriteTo.Console(
        outputTemplate: "[{Timestamp:HH:mm:ss} ({Level:u3})] [CorrelationId: {CorrelationId}] {Message:lj}{NewLine}{Exception}")
    .WriteTo.File("Logs/log-.txt",
        rollingInterval: RollingInterval.Day,
        outputTemplate: "[{Timestamp:yyyy-MM-dd HH:mm:ss} ({Level:u3})] [CorrelationId: {CorrelationId}] {Message:lj}{NewLine}{Exception}")
    .WriteTo.Seq("http://localhost:5341")
    .CreateLogger();

try
{
    Log.Information("Starting up the application...");

    var builder = WebApplication.CreateBuilder(args);
    builder.Host.UseSerilog();

    builder.Services.AddControllers();
    builder.Services.AddEndpointsApiExplorer();

    builder.Services.AddSwaggerGen(options =>
    {
        options.SwaggerDoc("v1", new()
        {
            Title = "FiapCloudGamesApi",
            Description = "Plataforma de venda de jogos digitais e gestão de servidores para partidas online.",
            Version = "v1"
        });
        options.DescribeAllParametersInCamelCase();
        options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
        {
            Name = "Authorization",
            Type = SecuritySchemeType.Http,
            Scheme = "bearer",
            BearerFormat = "JWT",
            In = ParameterLocation.Header,
            Description = "Insira o token JWT sem o prefixo 'Bearer'."
        });
        options.AddSecurityRequirement(new OpenApiSecurityRequirement
        {
        { new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] { }
        }
        });

        options.OperationFilter<OperationDescriptionFilter>();

    });

    builder.Services.AddAuthentication(x =>
    {
        x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
        .AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = builder.Configuration["Jwt:Issuer"],
                ValidAudience = builder.Configuration["Jwt:Audience"],
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
            };

            options.Events = new CustomJwtBearerEvents();

        });

    builder.Services.AddMemoryCache();
    builder.Services.RegisterServices(builder.Configuration);

    builder.Services.AddDbContext<ApplicationDbContext>(options =>
    {
        var connectionString = builder.Configuration.GetConnectionString("ConnectionString");
        options.UseNpgsql(connectionString);
    }, ServiceLifetime.Scoped);

    var app = builder.Build();

    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseMiddleware<ExceptionMiddleware>();
    app.UseMiddleware<CorrelationMiddleware>();
    app.UseHttpsRedirection();
    app.UseAuthentication();
    app.UseMiddleware<ValidateUserExistsMiddleware>();
    app.UseAuthorization();
    app.UseCustomStatusCodePages();
    app.MapControllers();

    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Application terminated unexpectedly");
}
finally
{
    Log.CloseAndFlush();
}