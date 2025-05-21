using Application.Interfaces;
using Application.Services;
using Core.Interfaces.Repository;
using Infrastructure.Repository;

namespace FiapCloudGamesApi.Configurations;

public static class DependencyInjectionConfig
{
    public static void RegisterServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddHttpContextAccessor();
        services.AddRepositories();
        services.AddAppServices();
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
        services.AddScoped<IAuthAppService, AuthAppService>();
        services.AddScoped<IGameAppService, GameAppService>();
        services.AddScoped<ILibraryAppService, LibraryAppService>();
        services.AddScoped<ICartAppService, CartAppService>();
        services.AddScoped<ICurrentUserAppService, CurrentUserAppService>();
    }
}
