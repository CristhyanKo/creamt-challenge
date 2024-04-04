using CreaMT.Domain.Entities;
using CreaMT.Domain.Repositories.Usuario;
using Microsoft.EntityFrameworkCore;

namespace CreaMT.infrastructure.DataAcess.Repositories;
public class UsuarioRepository : IUsuarioReadOnlyRepository, IUsuarioWriteOnlyRepository
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

    public async Task<Usuario?> GetByEmailAndPassword(string email, string password)
    {
        return await _dbContext
            .Usuarios
            .AsNoTracking()
            .FirstOrDefaultAsync(usuario => usuario.Ativo && usuario.Email.Equals(email) && usuario.Senha.Equals(password) && usuario.Excluido == false);
    }
}
