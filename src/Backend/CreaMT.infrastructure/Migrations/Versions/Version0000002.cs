using ApiCreaMT.Infrastructure.Migrations;
using FluentMigrator;

namespace CreaMT.infrastructure.Migrations.Versions;
[Migration(DatabaseVersions.TABLE_CLIENTE, "Cria tabela cliente")]
public class Version0000002 : VersionBase
{
    public override void Up()
    {
        CreateTable("Clientes")
            .WithColumn("Nome").AsString(100).NotNullable()
            .WithColumn("Email").AsString(255).NotNullable()
            .WithColumn("Telefone").AsString(11).NotNullable()
            .WithColumn("CpfCnpj").AsString(14).NotNullable()
            .WithColumn("Anuidade_Paga").AsBoolean().NotNullable()
            .WithColumn("UsuarioId").AsInt64().NotNullable().ForeignKey("FK_Cliente_Usuario_Id", "Usuarios","Id");
    }
}
