namespace CreaMT.Domain.Repositories.Documento;
public interface IDocumentoReadOnlyRepository
{
    public Task<IList<Entities.Documento>> GetAll();
}
