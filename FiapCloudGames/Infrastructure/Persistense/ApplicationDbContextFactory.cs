using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Persistense;
public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
{
    public ApplicationDbContext CreateDbContext(string[] args)
    {
        var optionsBuider = new DbContextOptionsBuilder<ApplicationDbContext>();

        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetParent(Directory.GetCurrentDirectory()).FullName)
            .AddJsonFile("FiapCloudGames\\appsettings.json", optional: true)
            .Build();

        var connectionString = configuration.GetConnectionString("ConnectionString");

        optionsBuider.UseNpgsql(connectionString);

        return new ApplicationDbContext(optionsBuider.Options, configuration);
    }
}
