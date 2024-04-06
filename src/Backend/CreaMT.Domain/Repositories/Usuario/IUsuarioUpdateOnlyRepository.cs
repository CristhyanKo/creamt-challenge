namespace CreaMT.Domain.Repositories.Usuario;
public interface IUsuarioUpdateOnlyRepository
{
    public Task<Entities.Usuario> GetById(long id);
    public void Update(Entities.Usuario usuario);
}
