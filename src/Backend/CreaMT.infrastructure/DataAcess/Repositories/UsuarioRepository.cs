using CreaMT.Domain.Entities;
using CreaMT.Domain.Repositories.Usuario;
using Microsoft.EntityFrameworkCore;

namespace CreaMT.infrastructure.DataAcess.Repositories;
public class UsuarioRepository : IUsuarioReadOnlyRepository, IUsuarioWriteOnlyRepository, IUsuarioUpdateOnlyRepository
{
    private readonly CreaMTAPIDbContext _dbContext;

    public UsuarioRepository(CreaMTAPIDbContext dbContext) => _dbContext = dbContext;

    public async Task Add(Usuario usuario) => await _dbContext.Usuarios.AddAsync(usuario);

    public async Task<bool> ExistActiveUsuarioWithEmail(string email)
    {
        return await _dbContext.Usuarios.AnyAsync(usuario => usuario.Email == email && usuario.Ativo && usuario.Excluido == false);
    }

    public async Task<bool> ExistActiveUsuarioWithCpfCnpj(string CpfCnpj)
    {
        return await _dbContext.Usuarios.AnyAsync(usuario => usuario.CpfCnpj == CpfCnpj && usuario.Ativo && usuario.Excluido == false);
    }
    public async Task<bool> ExistActiveUsuarioWithIdentifier(Guid UsuarioIdentifier)
    {
        return await _dbContext.Usuarios.AnyAsync(usuario => usuario.UsuarioIdentifier.Equals(UsuarioIdentifier) && usuario.Ativo && usuario.Excluido == false);
    }

    public async Task<Usuario?> GetByEmailAndPassword(string email, string password)
    {
        return await _dbContext
            .Usuarios
            .AsNoTracking()
            .FirstOrDefaultAsync(usuario => usuario.Ativo && usuario.Email.Equals(email) && usuario.Senha.Equals(password) && usuario.Excluido == false);
    }

    public async Task<Usuario> GetById(long id)
    {
        return await _dbContext
            .Usuarios
            .FirstAsync(usuario => usuario.Id == id && usuario.Ativo && usuario.Excluido == false);
    }

    public void Update(Usuario usuario) => _dbContext.Usuarios.Update(usuario);

    public async Task<IList<Usuario>> GetAll()
    {
        return await _dbContext
             .Usuarios
             .AsNoTracking()
             .ToListAsync();
    }

    public async Task Delete(long usuarioId)
    {
        var usuario = await GetById(usuarioId);
        usuario.Excluido = true;
        _dbContext.Update(usuario);
    }
}
