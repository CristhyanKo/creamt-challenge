using FluentMigrator;
using FluentMigrator.Builders.Create.Table;

namespace ApiCreaMT.Infrastructure.Migrations;
public abstract class VersionBase : ForwardOnlyMigration
{
    protected ICreateTableColumnOptionOrWithColumnSyntax CreateTable(string table)
    {
        return Create.Table(table)
                .WithColumn("Id").AsInt64().PrimaryKey().Identity()
                .WithColumn("DataCadastro").AsDateTime().NotNullable()
                .WithColumn("DataAtualizacao").AsDateTime().Nullable()
                .WithColumn("Ativo").AsBoolean().NotNullable()
                .WithColumn("Excluido").AsBoolean().NotNullable();
    }
}
