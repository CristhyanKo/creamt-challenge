using Microsoft.Extensions.Configuration;
namespace CreaMT.infrastructure.Extension;
public static class ConfigurationExtension
{
    public static bool IsUnitTestEnviroment(this IConfiguration configuration)
    {
        return configuration.GetValue<bool>("InMemoryTest");
    }
    public static string GetNomeDatabase(this IConfiguration configuration)
    {
        var nomeDatabase = configuration.GetConnectionString("NomeDatabase");
        return nomeDatabase;
    }
    public static string GetConexao(this IConfiguration configuration)
    {
        var conexao = configuration.GetConnectionString("Conexao");
        return conexao;
    }

    public static string GetConexaoCompleta(this IConfiguration configuration)
    {
        var nomeDatabase = configuration.GetNomeDatabase();
        var conexao = configuration.GetConexao();
        return $"{conexao};Database={nomeDatabase}";
    }
}
