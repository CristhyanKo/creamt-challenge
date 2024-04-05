namespace CreaMT.Domain.Repositories.SolicitacoesDocumentos;
public interface ISolicitacaoDocumentosWriteOnlyRepository
{
    public Task Add(Entities.SolicitacaoDocumento solicitacaoDocumento);
}
