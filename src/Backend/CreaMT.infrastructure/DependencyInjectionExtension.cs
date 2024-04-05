using CreaMT.Domain.Repositories;
using CreaMT.Domain.Repositories.Cliente;
using CreaMT.Domain.Repositories.Documento;
using CreaMT.Domain.Repositories.Servico;
using CreaMT.Domain.Repositories.Usuario;
using CreaMT.Domain.Security.Cryptography;
using CreaMT.Domain.Security.Tokens;
using CreaMT.Domain.Services.LoggerUser;
using CreaMT.infrastructure.DataAcess;
using CreaMT.infrastructure.DataAcess.Repositories;
using CreaMT.infrastructure.Extension;
using CreaMT.infrastructure.Security.Access.Generator;
using CreaMT.infrastructure.Security.Access.Validator;
using CreaMT.infrastructure.Security.Cryptography;
using CreaMT.infrastructure.Services.LoggedUsuario;
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
        AddPasswordsEncrypter(services, configuration);
        AddRepositories(services);
        AddLoggedUsuario(services);
        AddTokens(services, configuration);

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
        services.AddScoped<IUsuarioUpdateOnlyRepository, UsuarioRepository>();
        services.AddScoped<IClienteWriteOnlyRepository, ClienteRepository>();

        services.AddScoped<IDocumentoWriteOnlyRepository, DocumentoRepository>();

        services.AddScoped<IServicoWriteOnlyRepository, ServicoRepository>();
    }

    private static void AddFluentMigrator(IServiceCollection services, IConfiguration configuration)
    {
        services.AddFluentMigratorCore().ConfigureRunner(options =>
        {
            options
            .AddSqlServer()
            .WithGlobalConnectionString(configuration.GetConexaoCompleta())
            .ScanIn(Assembly.Load("CreaMT.infrastructure")).For.All();

        });
    }

    private static void AddTokens(IServiceCollection services, IConfiguration configuration)
    {
        var expirationTimeMinutes = configuration.GetValue<uint>("Settings:Jwt:ExpirationTimeMinutes");
        var signingKey = configuration.GetValue<string>("Settings:Jwt:signingKey");

        services.AddScoped<IAcessTokenGenerator>(option => new JwtTokenGenerator(expirationTimeMinutes, signingKey!));
        services.AddScoped<IAccessTokenValidator>(option => new JwtTokenValidator(signingKey!));
    }

    private static void AddLoggedUsuario(IServiceCollection services)
    {
        services.AddScoped<ILoggedUsuario, LoggedUsuario>();
    }

    private static void AddPasswordsEncrypter(IServiceCollection services, IConfiguration configuration)
    {
        var additionalKey = configuration.GetValue<string>("Settings:Password:AdditionalKey");
        services.AddScoped<IPasswordEncripter>(option => new Sha512Encripter(additionalKey!));
    }
}
