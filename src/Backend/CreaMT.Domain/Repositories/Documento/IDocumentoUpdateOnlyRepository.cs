namespace CreaMT.Domain.Repositories.Documento;
public interface IDocumentoUpdateOnlyRepository
{
    public Task<IList<Entities.Documento>> GetAllFromService(long servicoId);

}
