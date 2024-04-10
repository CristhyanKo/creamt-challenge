namespace CreaMT.Domain.Repositories.Servico;
public interface IServicoReadOnlyRepository
{
    public Task<IList<Entities.Servico>> GetAll();
}
