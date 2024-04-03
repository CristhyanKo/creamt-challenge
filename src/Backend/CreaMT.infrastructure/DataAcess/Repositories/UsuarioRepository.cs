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
        return await _dbContext.Usuarios.AnyAsync(u => u.Email == email && u.Ativo && u.Excluido == false);
    }

    public async Task<bool> ExistActiveUsuarioWithCpfCnpj(string CpfCnpj)
    {
        return await _dbContext.Usuarios.AnyAsync(u => u.CpfCnpj == CpfCnpj && u.Ativo && u.Excluido == false);
    }

}
