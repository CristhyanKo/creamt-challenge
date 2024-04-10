using ApiCreaMT.Infrastructure.Migrations;
using FluentMigrator;

namespace CreaMT.infrastructure.Migrations.Versions;
[Migration(DatabaseVersions.TABLE_SERVICO, "Cria tabela serviço")]
public class Version0000003 : VersionBase
{
    public override void Up()
    {
        CreateTable("Servicos")
            .WithColumn("Nome").AsString(100).NotNullable()
            .WithColumn("Descricao").AsString(300).NotNullable()
            .WithColumn("Valor").AsDecimal(8, 2).NotNullable()
            .WithColumn("UsuarioId").AsInt64().NotNullable().ForeignKey("FK_Servico_Usuario_Id", "Usuarios", "Id");
    }
}