namespace CreaMT.Domain.Repositories.Usuario;
public interface IUsuarioReadOnlyRepository
{
    public Task<bool> ExistActiveUsuarioWithCpfCnpj(string email);
    public Task<bool> ExistActiveUsuarioWithEmail(string CpfCnpj);

    public Task<Entities.Usuario?> GetByEmailAndPassword(string email, string password);

}
