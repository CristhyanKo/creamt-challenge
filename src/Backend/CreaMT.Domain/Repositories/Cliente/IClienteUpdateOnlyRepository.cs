namespace CreaMT.Domain.Repositories.Cliente;
public interface IClienteUpdateOnlyRepository
{
    public Task<Entities.Cliente> GetById(long id);
    public void Update(Entities.Cliente cliente);
}
