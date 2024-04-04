using CreaMT.infrastructure.Migrations;
using FluentMigrator;

namespace ApiCreaMT.Infrastructure.Migrations.Versoes;
[Migration(DatabaseVersions.TABLE_USUARIO, "Cria tabela usuario")]
public class Versao0000001 : VersionBase
{
    public override void Up()
    {
        CreateTable("Usuarios")
            .WithColumn("Nome").AsString(100).NotNullable()
            .WithColumn("Email").AsString(255).NotNullable()
            .WithColumn("Telefone").AsString(11).NotNullable()
            .WithColumn("CpfCnpj").AsString(14).NotNullable()
            .WithColumn("Senha").AsString(2000).NotNullable();
    }
}
