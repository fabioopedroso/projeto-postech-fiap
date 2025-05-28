using Application.Contracts;
using Application.Interfaces;
using Application.Interfaces.Cache;
using Application.Services;
using Application.Services.Cache;
using Core.Interfaces.Repository;
using Infrastructure.Authentication;
using Infrastructure.Identity;
using Infrastructure.Repository;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


namespace Infrastructure.IoC;

public static class DependencyInjectionConfig
{
    public static void RegisterServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddHttpContextAccessor();
        services.AddRepositories();
        services.AddAppServices();
        services.AddServices();
        services.AddCacheServices();
    }

    private static void AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IGameRepository, GameRepository>();
        services.AddScoped<ISaleRepository, SaleRepository>();
        services.AddScoped<ILibraryRepository, LibraryRepository>();
        services.AddScoped<ICartRepository, CartRepository>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
    }

    private static void AddAppServices(this IServiceCollection services)
    {
        services.AddScoped<IUserAppService, UserAppService>();
        services.AddScoped<IGameAppService, GameAppService>();
        services.AddScoped<ILibraryAppService, LibraryAppService>();
        services.AddScoped<ICartAppService, CartAppService>();
        services.AddScoped<ICheckoutAppService, CheckoutAppService>();
        services.AddScoped<IUserAdminAppService, UserAdminAppService>();
    }

    private static void AddServices(this IServiceCollection services)
    {
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<ICurrentUseService, CurrentUserService>();
    }

    private static void AddCacheServices(this IServiceCollection services)
    {
        services.AddScoped<ILibraryCacheService, LibraryCacheService>();
    }
}
