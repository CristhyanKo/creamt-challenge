using ApiCreaMT.Infrastructure.Migrations;
using FluentMigrator;

namespace CreaMT.infrastructure.Migrations.Versions;
[Migration(DatabaseVersions.TABLE_SOLICITACOES, "Cria tabela solicitações")]
public class Version0000005 : VersionBase
{
    public override void Up()
    {
        CreateTable("Solicitacoes")
            .WithColumn("ServicoId").AsInt64().NotNullable().ForeignKey("FK_Solicitacoe_Servico_Id", "Servicos", "Id")
            .WithColumn("ClienteId").AsInt64().NotNullable().ForeignKey("FK_Solicitacoe_Cliente_Id", "Clientes", "Id")
            .WithColumn("UsuarioId").AsInt64().NotNullable().ForeignKey("FK_Solicitacoes_Usuario_Id", "Usuarios", "Id")
            .WithColumn("Pago").AsBoolean().NotNullable();
    }
}