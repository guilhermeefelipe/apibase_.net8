using ApiBase.Repositories;
using ApiBase.Repositories.DbContexts;
using ApiBase.Repositories.Repositories;
using ApiBase.Repositories.Repositories.Products;
using ApiBase.Repositories.Repositories.Sectors;
using Microsoft.Extensions.Configuration;

namespace Microsoft.Extensions.DependencyInjection;

public static class Extensions
{
    public static void AddApiBaseRepositories(this IServiceCollection services, IConfiguration configuration)
    {
        var provider = configuration["Provider"];
        switch (provider)
        {
            case "SqlServer":
                services.AddDbContext<ApiBaseDbContext, SqlServerDbContext>();
                break;
            case "MySql":
                services.AddDbContext<ApiBaseDbContext, MySqlDbContext>();
                break;
            default:
                throw new InvalidOperationException($"Provedor de banco de dados '{provider}' não suportado.");
        }

        services.AddScoped<IDatabaseMigrator, DatabaseMigrator>();

        services.AddDbContext<SqlServerDbContext>();
        services.AddDbContext<MySqlDbContext>();

        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<ISectorRepository, SectorRepository>();

    }
}
