using Dapper;
using FluentMigrator;
using FluentMigrator.Runner;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.DependencyInjection;
using CreaMT.Domain.Extensions;

namespace CreaMT.infrastructure.Migrations;
public static class DatabaseMigration
{
    public static void Migrat(string connectionString, IServiceProvider serviceProvider)
    {
        EnsureDatabaseCreate_SqlServer(connectionString);
        MigrationDatabase(serviceProvider);
    }


    private static void EnsureDatabaseCreate_SqlServer(string connectionString)
    {
        var connectionStringBuilder = new SqlConnectionStringBuilder(connectionString);
        var databaseName = connectionStringBuilder.InitialCatalog;
        connectionStringBuilder.Remove("Database");
        using var dbConnection = new SqlConnection(connectionStringBuilder.ConnectionString);

        var parameters = new DynamicParameters();
        parameters.Add("databaseName", databaseName);

        var records = dbConnection.Query("SELECT * FROM sys.databases WHERE name = @databaseName", parameters);
        if(records.Any().IsFalse())
        {
            dbConnection.Execute($"CREATE DATABASE {databaseName}");
        }
    }

    private static void MigrationDatabase(IServiceProvider serviceProvider)
    {
        var runner = serviceProvider.GetRequiredService<IMigrationRunner>();
        runner.ListMigrations();
        runner.MigrateUp();
    }
}
