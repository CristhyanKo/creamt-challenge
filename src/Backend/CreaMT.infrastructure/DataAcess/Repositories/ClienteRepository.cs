using CreaMT.Domain.Entities;
using CreaMT.Domain.Repositories.Cliente;

namespace CreaMT.infrastructure.DataAcess.Repositories;
public class ClienteRepository : IClienteWriteOnlyRepository
{
    private readonly CreaMTAPIDbContext _dbContext;

    public ClienteRepository(CreaMTAPIDbContext dbContext) => _dbContext = dbContext;

    public async Task Add(Cliente cliente)
    {
        await _dbContext.Clientes.AddAsync(cliente);
    }
}
