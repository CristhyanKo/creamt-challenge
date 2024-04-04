using CreaMT.Domain.Repositories;
using CreaMT.Domain.Repositories.Usuario;
using CreaMT.infrastructure.DataAcess;
using CreaMT.infrastructure.DataAcess.Repositories;
using CreaMT.infrastructure.Extension;
using FluentMigrator.Runner;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace CreaMT.infrastructure;
public static class DependencyInjectionExtension
{
    public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        AddRepositories(services);
        if (configuration.IsUnitTestEnviroment())
            return;
 
        AddFluentMigrator(services, configuration);
        AddDbContext(services, configuration);
    }

    private static void AddDbContext(IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<CreaMTAPIDbContext>(dbContextOptions =>
        {
            dbContextOptions.UseSqlServer(configuration.GetConexaoCompleta());
        });
    }

    private static void AddRepositories(IServiceCollection services)
    {
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IUsuarioReadOnlyRepository, UsuarioRepository>();
        services.AddScoped<IUsuarioWriteOnlyRepository, UsuarioRepository>();
    }

    private static void AddFluentMigrator(IServiceCollection services, IConfiguration configuration)
    {
        services.AddFluentMigratorCore().ConfigureRunner(options => {
            options
            .AddSqlServer()
            .WithGlobalConnectionString(configuration.GetConexaoCompleta())
            .ScanIn(Assembly.Load("CreaMT.infrastructure")).For.All();

        });
    }
}
