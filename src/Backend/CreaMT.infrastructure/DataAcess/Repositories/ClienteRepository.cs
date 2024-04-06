using CreaMT.Domain.Entities;
using CreaMT.Domain.Repositories.Cliente;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace CreaMT.infrastructure.DataAcess.Repositories;
public class ClienteRepository : IClienteWriteOnlyRepository, IClienteUpdateOnlyRepository, IClienteReadOnlyRepository
{
    private readonly CreaMTAPIDbContext _dbContext;

    public ClienteRepository(CreaMTAPIDbContext dbContext) => _dbContext = dbContext;

    public async Task Add(Cliente cliente)
    {
        await _dbContext.Clientes.AddAsync(cliente);
    }

    public async Task<Cliente> GetById(long id)
    {
        return await _dbContext
            .Clientes
            .FirstAsync(cliente => cliente.Id == id && cliente.Ativo && cliente.Excluido == false);
    }

    public void Update(Cliente cliente) => _dbContext.Clientes.Update(cliente);


    public async Task<bool> ExistActiveClienteWithCpfCnpj(string email)
    {
        return await _dbContext
            .Clientes
            .AnyAsync(cliente => cliente.Email == email && cliente.Ativo && cliente.Excluido == false);
    }

    public async  Task<bool> ExistActiveClienteWithEmail(string CpfCnpj)
    {
        return await _dbContext
            .Clientes
            .AnyAsync(cliente => cliente.CpfCnpj == CpfCnpj && cliente.Ativo && cliente.Excluido == false);
    }

    public async Task<IList<Cliente>> GetAll()
    {
        return await _dbContext
           .Clientes
           .AsNoTracking()
           .ToListAsync();
    }
}
