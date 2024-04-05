using ApiCreaMT.Infrastructure.Migrations;
using FluentMigrator;

namespace CreaMT.infrastructure.Migrations.Versions;
[Migration(DatabaseVersions.TABLE_DOCUMENTO, "Cria tabela documento")]
public class Version0000004 : VersionBase
{
    public override void Up()
    {
        CreateTable("Documentos")
            .WithColumn("Nome").AsString(100).NotNullable()
            .WithColumn("EnderecoArquivo").AsString(300).NotNullable()
            .WithColumn("Downloads").AsInt32().NotNullable()
            .WithColumn("ServicoId").AsInt64().NotNullable().ForeignKey("FK_Documento_Servico_Id", "Servicos", "Id")
            .WithColumn("UsuarioId").AsInt64().NotNullable().ForeignKey("FK_Documento_Usuario_Id", "Usuarios", "Id");
    }
}