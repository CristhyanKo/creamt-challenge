namespace CreaMT.Domain.Repositories.Usuario;
public interface IUsuarioReadOnlyRepository
{
    public Task<bool> ExistActiveUsuarioWithCpfCnpj(string email);
    public Task<bool> ExistActiveUsuarioWithEmail(string CpfCnpj);
    public Task<bool> ExistActiveUsuarioWithIdentifier(Guid UsuarioIdentifier);

    public Task<Entities.Usuario?> GetByEmailAndPassword(string email, string password);

    public Task<IList<Entities.Usuario>> GetAll();
}
