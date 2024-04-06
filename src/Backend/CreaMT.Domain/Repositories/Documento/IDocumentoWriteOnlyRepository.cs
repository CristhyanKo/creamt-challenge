namespace CreaMT.Domain.Repositories.Documento;
public interface IDocumentoWriteOnlyRepository
{
    public Task Add(Entities.Documento documento);
}
