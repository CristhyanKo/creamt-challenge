using ApiCreaMT.Infrastructure.Migrations;
using FluentMigrator;

namespace CreaMT.infrastructure.Migrations.Versions;
[Migration(DatabaseVersions.TABLE_SOLICITACOES_DOCUMENTO, "Cria tabela de associação entre Solicitacao e Documento")]
public class Version0000006 : VersionBase
{
    public override void Up()
    {
        CreateTable("SolicitacoesDocumentos")
            .WithColumn("SolicitacaoId").AsInt64().NotNullable().ForeignKey("FK_SolicitacoesDocumentos_Solicitacoes_Id", "Solicitacoes", "Id")
            .WithColumn("DocumentoId").AsInt64().NotNullable().ForeignKey("FK_SolicitacoesDocumentos_Documento_Id", "Documentos", "Id")
            .WithColumn("UsuarioId").AsInt64().NotNullable().ForeignKey("FK_SolicitacoesDocumentos_Usuario_Id", "Usuarios", "Id")
            .WithColumn("Downloads").AsInt32().NotNullable();
    }
}