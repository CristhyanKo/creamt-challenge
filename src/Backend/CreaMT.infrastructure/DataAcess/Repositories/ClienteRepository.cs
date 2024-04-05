using CreaMT.Domain.Entities;
using CreaMT.Domain.Repositories.Cliente;
using Microsoft.EntityFrameworkCore;

namespace CreaMT.infrastructure.DataAcess.Repositories;
public class ClienteRepository : IClienteWriteOnlyRepository, IClienteUpdateOnlyRepository
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
}
