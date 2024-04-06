namespace CreaMT.Domain.Repositories.Solicitacao;
public interface ISolicitacaoWriteOnlyRepository
{
    public Task Add(Entities.Solicitacao solicitacao);
}
