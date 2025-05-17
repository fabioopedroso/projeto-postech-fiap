using Core.Interfaces.Repository;
using Infrastructure.Repository;

namespace FiapCloudGamesApi.Configurations;

public static class DependencyInjectionConfig
{
    public static void RegisterServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddRepositories();
        services.AddServices();
    }

    private static void AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IGameRepository, GameRepository>();
        services.AddScoped<ISaleRepository, SaleRepository>();
    }

    private static void AddServices(this IServiceCollection services)
    {
        
    }
}
