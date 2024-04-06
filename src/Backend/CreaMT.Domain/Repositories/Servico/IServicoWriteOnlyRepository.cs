namespace CreaMT.Domain.Repositories.Servico;
public interface IServicoWriteOnlyRepository
{
    public Task Add(Entities.Servico servico);
}
