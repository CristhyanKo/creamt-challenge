namespace CreaMT.Domain.Repositories.Cliente;
public interface IClienteWriteOnlyRepository
{
    public Task Add(Entities.Cliente cliente);
}
