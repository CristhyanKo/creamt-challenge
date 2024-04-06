namespace CreaMT.Domain.Repositories.Cliente;
public interface IClienteReadOnlyRepository
{
    public Task<bool> ExistActiveClienteWithCpfCnpj(string email);
    public Task<bool> ExistActiveClienteWithEmail(string CpfCnpj);
    public Task<IList<Entities.Cliente>> GetAll();

}
